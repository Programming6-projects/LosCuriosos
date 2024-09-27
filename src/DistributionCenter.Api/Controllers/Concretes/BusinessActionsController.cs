namespace DistributionCenter.Api.Controllers.Concretes;

using Application.Constants;
using Application.Repositories.Interfaces;
using Commons.Results;
using Domain.Entities.Concretes;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Services.Distribution.Enums;
using Services.Distribution.Interfaces;
using Services.Localization.Commons;
using Services.Routes.Dtos;
using Services.Routes.Interfaces;

[Route("api/business-actions")]
public class BusinessActionsController(
    IRepository<Order> orderRepository,
    IRepository<Transport> transportRepository,
    IRepository<Trip> tripRepository,
    IDistributionStrategy distributionStrategy,
    IRepository<DeliveryPoint> deliveryPointRepository,
    IRouteService routeService
    ) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> StartDistribution([FromForm] Location location)
    {
        Result<IEnumerable<Order>> resultOrders = await orderRepository
            .SelectWhereAsync(x => x is { IsActive: true, Status: Status.Pending });

        if (!resultOrders.IsSuccess)
        {
            return this.ErrorsResponse(resultOrders.Errors);
        }

        Result<IEnumerable<Transport>> resultTransports = await transportRepository
            .SelectWhereAsync(x => x is { IsActive: true, IsAvailable: true });

        if (!resultTransports.IsSuccess)
        {
            return this.ErrorsResponse(resultTransports.Errors);
        }

        ICollection<Order> orders = resultOrders.Value.ToList();
        ICollection<Transport> transports = resultTransports.Value.ToList();

        (IEnumerable<Trip> Trips,
            IEnumerable<Transport> UpdatedTransports,
            IEnumerable<Order> CancelledOrders
            ) distributionResult = distributionStrategy.DistributeOrders(orders, transports, location);

        _ = await UpdateTables(orders, distributionResult);

        return Ok(new DistributionResult
        {
            OrdersAssigned = orders.Count - distributionResult.CancelledOrders.Count(),
            Trips = distributionResult.Trips,
            TripsCount = distributionResult.Trips.Count(),
            UpdatedTransports = distributionResult.UpdatedTransports,
            UpdatedTransportsCount = distributionResult.UpdatedTransports.Count(),
            CancelledOrders = distributionResult.CancelledOrders,
            CancelledOrdersCount = distributionResult.CancelledOrders.Count()
        });

    }

    [NonAction]
    public async Task<int> UpdateTables(
        IEnumerable<Order> orders,
        (IEnumerable<Trip> Trips,
        IEnumerable<Transport> UpdatedTransports,
        IEnumerable<Order> CancelledOrders) distributionResult)
    {
        int rowsAffected = 0;

        distributionResult.Trips = await GetOrdersOrderedTrips(distributionResult.Trips);
        foreach (Order order in distributionResult.Trips.ToList()[0].Orders)
        {
            Console.WriteLine(order.DeliveryPointId + " " + order.DeliveryTime );
        }

        IEnumerable<Transport> transportsNewStatus = distributionResult.UpdatedTransports.Select(x =>
        {
            x.IsAvailable = false;
            return x;
        });

        IEnumerable<Order> ordersAccepted = distributionResult.Trips.SelectMany(trip => trip.Orders).Select(
            x =>
            {
                x.Status = Status.Sending;
                return x;
            });

        Result<int> transportsAffected =  await transportRepository.UpdateAllAsync(transportsNewStatus);
        Result<int> tripAffected = await tripRepository.CreateAllAsync(distributionResult.Trips);

        Result<int> ordersAcceptedAffected = await orderRepository.UpdateAllAsync(
            ordersAccepted.Select(x =>
            {
                x.Status = Status.Sending;
                return x;
            })
        );
        Result<int> ordersCancelledAffected = await orderRepository.UpdateAllAsync(
            distributionResult.CancelledOrders.Select(x =>
            {
                x.Status = Status.Cancelled;
                return x;
            })
        );

        rowsAffected =
            transportsAffected.Value
            + tripAffected.Value
            + ordersAcceptedAffected.Value
            + ordersCancelledAffected.Value;

        return rowsAffected;
    }

    private async Task<IEnumerable<Trip>> GetOrdersOrderedTrips(IEnumerable<Trip> trips)
    {
        List<Trip> orderedTrips = new();

        foreach (Trip trip in trips)
        {
            Result<IReadOnlyList<WayPointDto>> wayPointsResult = await GetOrdersOrderByCloserLocationToUpdateTrip(trip);
            if (!wayPointsResult.IsSuccess) continue;

            IReadOnlyList<WayPointDto> wayPoints = wayPointsResult.Value;
            ICollection<Order> listOrders = trip.Orders;

            Console.WriteLine(wayPoints.Count + " = " + listOrders.Count + " qaaaaaa");
            Dictionary<GeoPoint, Order> geoPointToOrderMap = new();

            foreach (Order order in listOrders)
            {
                Result<DeliveryPoint> deliveryPointResult = await deliveryPointRepository.GetByIdAsync(order.DeliveryPointId);
                if (!deliveryPointResult.IsSuccess) continue;

                Console.WriteLine(deliveryPointResult.Value.Latitude + " hhhhhh");
                DeliveryPoint deliveryPoint = deliveryPointResult.Value;
                GeoPoint geoPoint = new(deliveryPoint.Latitude, deliveryPoint.Longitude);
                geoPointToOrderMap[geoPoint] = order;
            }

            Console.WriteLine(geoPointToOrderMap.Count + " jajajaja");
            List<Order> orderedOrders = new();
            foreach (WayPointDto wayPoint in wayPoints)
            {
                Console.WriteLine("Sdasdsadsadsadsadsadsadsadsadasdsa " );
                if (geoPointToOrderMap.TryGetValue(wayPoint.Point, out Order? matchingOrder))
                {
                    Console.WriteLine("1111111111111111111111111111111111111111111");
                    matchingOrder.DeliveryTime = wayPoint.DeliverTime;
                    Console.WriteLine(matchingOrder.DeliveryTime + " = " + matchingOrder.DeliveryPointId);
                    orderedOrders.Add(matchingOrder);
                }
            }

            Console.WriteLine(orderedOrders.Count + "Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            Trip orderedTrip = new()
            {
                Id = trip.Id,
                TransportId = trip.TransportId,
                Orders = orderedOrders,
                Status = Status.Pending,
            };

            Console.WriteLine("=== " + orderedOrders.Count + " \n\n ===");
            orderedTrips.Add(orderedTrip);
        }

        return orderedTrips;
    }

    private async Task<Result<IReadOnlyList<WayPointDto>>> GetOrdersOrderByCloserLocationToUpdateTrip(
        Trip trip)
    {
        GeoPoint startPoint = new(DbConstants.BusinessLocation[0],DbConstants.BusinessLocation[1]);
        DateTime actualTime = DateTime.Now;
        List<GeoPoint> geoPoints = [];

        IEnumerable<Task<GeoPoint?>> deliveryPointTasks = trip.Orders
            .Select(async order =>
            {
                Result<DeliveryPoint> deliveryPointResult = await deliveryPointRepository.GetByIdAsync(
                    order.DeliveryPointId);
                if (!deliveryPointResult.IsSuccess) return null;

                DeliveryPoint deliveryPoint = deliveryPointResult.Value;
                return new GeoPoint(deliveryPoint.Latitude, deliveryPoint.Longitude);
            });

        GeoPoint?[] deliveryPoints = await Task.WhenAll(deliveryPointTasks);
        geoPoints.AddRange(deliveryPoints.Where(point => point != null)!);
        return await routeService.GetOptimalRoute(startPoint, geoPoints, actualTime);
    }
}
