namespace DistributionCenter.Api.Controllers.Concretes;

using Application.Repositories.Interfaces;
using Commons.Results;
using Domain.Entities.Concretes;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Services.Distribution.Enums;
using Services.Distribution.Interfaces;

[Route("api/business-actions")]
public class BusinessActionsController(
    IRepository<Order> orderRepository,
    IRepository<Transport> transportRepository,
    IRepository<Trip> tripRepository,
    IDistributionStrategy distributionStrategy
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
}
