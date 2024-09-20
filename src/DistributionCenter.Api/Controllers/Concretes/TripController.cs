namespace DistributionCenter.Api.Controllers.Concretes;

using System.ComponentModel.DataAnnotations;
using Application.Constants;
using Application.Contexts.Concretes;
using Application.Repositories.Interfaces;
using Application.Tables.Connections.File.Concretes;
using Application.Tables.Core.Concretes;
using Bases;
using Commons.Errors;
using Commons.Results;
using Domain.Entities.Concretes;
using Domain.Entities.Enums;
using Extensions;
using Infraestructure.DTOs.Concretes.Trip;
using Microsoft.AspNetCore.Mvc;

[Route("api/trips")]
public class TripController(IRepository<Trip> repository)
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
            allOrdersComplete = order.Status != Status.Shipped;
        }

        Result<Trip> result = await Repository.UpdateAsync(entity);

        if (allOrdersComplete)
        {
            return await UpdateAvailabilityOfTransport(true, entity.TransportId);
        }

        if (result.IsSuccess)
        {
            return result.Match(entity => Ok(entity), this.ErrorsResponse);
        }
        return this.ErrorsResponse(result.Errors);
    }

    private async Task<IActionResult> UpdateAvailabilityOfTransport(bool availability, Guid? transportId)
    {
        if (transportId == null)
        {
            return this.ErrorsResponse([Error.Conflict()]);
        }

        TransportRepository transportRepository
            = new (new Context(
                new Dictionary<Type, object>
                {
                    { typeof(Transport), new TransportTable(new JsonConnectionFactory<Transport>(
                        DbConstants.TransportSchema)) }
                }));

        Result<Transport> transportResult = await transportRepository.GetByIdAsync((Guid) transportId);

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
