namespace DistributionCenter.Api.Controllers.Concretes;

using System.ComponentModel.DataAnnotations;
using Application.Repositories.Interfaces;
using Bases;
using Commons.Errors;
using Commons.Results;
using Domain.Entities.Concretes;
using Extensions;
using Infraestructure.DTOs.Concretes.Trip;
using Microsoft.AspNetCore.Mvc;

[Route("api/trips")]
public class TripController(IRepository<Trip> repository, IRepository<Transport> transportRepository)
    : BaseEntityController<Trip, CreateTripDto, UpdateTripDto> (repository)
{
    [HttpPut("complete-trip/{tripId}")]
    public async Task<IActionResult> CompleteTrip([FromRoute] [Required] Guid tripId)
    {

        Result<Trip> searchEntity = await Repository.GetByIdAsync(tripId);

        if (!searchEntity.IsSuccess)
        {
            return this.ErrorsResponse(searchEntity.Errors);
        }

        Trip entity = searchEntity.Value;
        bool allOrdersComplete = true;
        foreach (Order order in entity.Orders)
        {
            if (order.Status != Status.Delivered)
            {
                allOrdersComplete = false;
                break;
            }
        }


        if (!allOrdersComplete)
        {
            return this.ErrorsResponse([Error.Conflict()]);
        }

        entity.Status = Status.Delivered;
        _ = await UpdateAvailabilityOfTransport(true, entity.TransportId);

        Result<Trip> result = await Repository.UpdateAsync(entity);

        if (result.IsSuccess)
        {
            return result.Match(entity => Ok(entity), this.ErrorsResponse);
        }
        return this.ErrorsResponse(result.Errors);
    }

    [NonAction]
    private async Task<IActionResult> UpdateAvailabilityOfTransport(bool availability, Guid? transportId)
    {
        if (transportId == null)
        {
            return this.ErrorsResponse([Error.Conflict()]);
        }

        Result<Transport> transportResult = await transportRepository.GetByIdAsync(transportId.Value);

        if (!transportResult.IsSuccess)
        {
            return this.ErrorsResponse(transportResult.Errors);
        }

        Transport transport = transportResult.Value;
        transport.IsAvailable = availability;

        Result<Transport> updateAsync = await transportRepository.UpdateAsync(transport);

        if (updateAsync.IsSuccess)
        {
            return updateAsync.Match(entity => Ok(entity), this.ErrorsResponse);
        }

        return this.ErrorsResponse(updateAsync.Errors);
    }
}
