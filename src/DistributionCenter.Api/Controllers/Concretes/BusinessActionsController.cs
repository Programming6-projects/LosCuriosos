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
using Services.Notification.Dtos;
using Services.Notification.Interfaces;
using Services.Routes.Dtos;
using Services.Routes.Interfaces;

[Route("api/business-actions")]
public class BusinessActionsController(
    IRepository<Order> orderRepository,
    IRepository<Transport> transportRepository,
    IRepository<Client> clientRepository,
    IRepository<Trip> tripRepository,
    IDistributionStrategy distributionStrategy,
    IRepository<DeliveryPoint> deliveryPointRepository,
    IEmailService emailService,
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
        distributionResult.Trips = await GetOrdersOrderedTrips(distributionResult.Trips);
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
                _ = SendNotificationOfUpdateOrders(x);
                return x;
            })
        );
        Result<int> ordersCancelledAffected = await orderRepository.UpdateAllAsync(
            distributionResult.CancelledOrders.Select(x =>
            {
                x.Status = Status.Cancelled;
                _ = SendNotificationOfUpdateOrders(x);
                return x;
            })
        );

        int rowsAffected = transportsAffected.Value
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
            Trip? orderedTrip = await CreateOrderedTrip(trip);
            if (orderedTrip != null) orderedTrips.Add(orderedTrip);
        }

        return orderedTrips;
    }

    private async Task<Trip?> CreateOrderedTrip(Trip trip)
    {
        Result<IReadOnlyList<WayPointDto>> wayPointsResult = await GetOrdersOrderByCloserLocationToUpdateTrip(trip);
        if (!wayPointsResult.IsSuccess) return null;

        Dictionary<GeoPoint, Order> geoPointToOrderMap = await CreateGeoPointToOrderMap(trip.Orders);
        List<Order> orderedOrders = OrderOrders(wayPointsResult.Value, geoPointToOrderMap);

        return new Trip
        {
            Id = trip.Id,
            TransportId = trip.TransportId,
            Orders = orderedOrders,
            Status = Status.Pending,
        };
    }

    private async Task<Dictionary<GeoPoint, Order>> CreateGeoPointToOrderMap(ICollection<Order> orders)
    {
        Dictionary<GeoPoint, Order> geoPointToOrderMap = new();

        foreach (Order order in orders)
        {
            Result<DeliveryPoint> deliveryPointResult = await deliveryPointRepository.GetByIdAsync(order.DeliveryPointId);
            if (!deliveryPointResult.IsSuccess) continue;

            DeliveryPoint deliveryPoint = deliveryPointResult.Value;
            GeoPoint geoPoint = new(deliveryPoint.Latitude, deliveryPoint.Longitude);
            geoPointToOrderMap.Add(geoPoint, order);
        }

        return geoPointToOrderMap;
    }

    private List<Order> OrderOrders(IReadOnlyList<WayPointDto> wayPoints, Dictionary<GeoPoint, Order> geoPointToOrderMap)
    {
        List<Order> orderedOrders = new();

        foreach (WayPointDto wayPoint in wayPoints)
        {
            if (geoPointToOrderMap.TryGetValue(wayPoint.Point, out Order? matchingOrder))
            {
                matchingOrder.DeliveryTime = wayPoint.DeliverTime;
                _ = SendNotificationOfUpdateOrders(matchingOrder);
                orderedOrders.Add(matchingOrder);
            }
        }

        return orderedOrders;
    }

    private async Task SendNotificationOfUpdateOrders(Order order)
    {
        Result<Client> clientResult = await clientRepository.GetByIdAsync(order.ClientId);
        Client client = clientResult.Value;

        OrderDto orderDto = new()
        {
            OrderId = order.Id,
            OrderStatus = order.Status,
            TimeToDeliver = order.DeliveryTime
        };

        IMessage message = NotificationFactory.CreateMessage(orderDto);
        _ = Task.Run(() => emailService.SendEmailAsync(client.Email, message));
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
