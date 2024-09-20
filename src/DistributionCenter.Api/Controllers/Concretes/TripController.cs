namespace DistributionCenter.Api.Controllers.Concretes;

using System.ComponentModel.DataAnnotations;
using Application.Repositories.Interfaces;
using Bases;
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
    [HttpPut("CompleteTrip")]
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
            return result.Match(entity => Ok(entity), this.ErrorsResponse);
        }

        return this.ErrorsResponse(result.Errors);
    }
}
