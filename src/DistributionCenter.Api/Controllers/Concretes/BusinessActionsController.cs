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
    IRepository<Order> repository,
    IDistributionStrategy distributionStrategy
    ) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> StartDistribution([FromForm] Location location)
    {
        Result<IEnumerable<Order>> resultOrders = await repository
            .SelectWhereAsync(x => (x.IsActive = true) && (x.Status == Status.Pending));

        if (!resultOrders.IsSuccess)
        {
            return this.ErrorsResponse(resultOrders.Errors);
        }

        TransportRepository transportRepository = new(repository.Context);
        Result<IEnumerable<Transport>> resultTransports = await transportRepository
            .SelectWhereAsync(x => x.IsActive = true);

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

        return Ok(distributionResult);
    }
}
