namespace DistributionCenter.Api.Controllers.Concretes;

using Application.Contexts.Interfaces;
using Application.Repositories.Interfaces;
using Commons.Results;
using Domain.Entities.Concretes;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Services.Distribution.Enums;
using Services.Distribution.Interfaces;

[Route("api/business-actions")]
public class BusinessActionsController(
    IRepository<Order> repository,
    IDistributionStrategy distributionStrategy
    ) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> StartDistribution([FromForm] Location location)
    {
        Result<IEnumerable<Order>> resultOrders = await repository
            .SelectWhereAsync(x => x is { IsActive: true, Status: Status.Pending });

        if (!resultOrders.IsSuccess)
        {
            return this.ErrorsResponse(resultOrders.Errors);
        }

        TransportRepository transportRepository = new(repository.Context);
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

        UpdateTables(orders, distributionResult, repository.Context);

        return Ok(new
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

    private static async void UpdateTables(
        IEnumerable<Order> orders,
        (IEnumerable<Trip> Trips,
        IEnumerable<Transport> UpdatedTransports,
        IEnumerable<Order> CancelledOrders) distributionResult,
        IContext context)
    {
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

        _ = await new TransportRepository(context).UpdateAllAsync(transportsNewStatus);
        _ = await new TripRepository(context).CreateAllAsync(distributionResult.Trips);

        OrderRepository orderRepository = new(context);
        _ = await orderRepository.UpdateAllAsync(
            ordersAccepted.Select(x =>
            {
                x.Status = Status.Sending;
                return x;
            })
        );
        _ = await orderRepository.UpdateAllAsync(
            distributionResult.CancelledOrders.Select(x =>
            {
                x.Status = Status.Cancelled;
                return x;
            })
        );
    }
}
