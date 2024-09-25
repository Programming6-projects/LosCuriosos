namespace DistributionCenter.Api.Controllers.Concretes;

using Application.Repositories.Interfaces;
using Bases;
using Commons.Results;
using Domain.Entities.Concretes;
using Extensions;
using Infraestructure.DTOs.Concretes.DeliveryPoint;
using Microsoft.AspNetCore.Mvc;
using Services.Localization.Commons;
using Services.Localization.Interfaces;

[Route("api/deliveryPoint")]
public class DeliveryPointController(
    IRepository<DeliveryPoint> repository,
    ILocationValidator locationValidator
) : BaseEntityController<DeliveryPoint, CreateDeliveryPointDto, UpdateDeliveryPointDto>(repository)
{
    [HttpPost("createDeliveryPoint")]
    public async Task<IActionResult> CreateDeliveryPoint(CreateDeliveryPointDto request)
    {
        Result validateResult = request.Validate();

        if (!validateResult.IsSuccess)
        {
            return this.ErrorsResponse(validateResult.Errors);
        }

        GeoPoint geoPoint = new(request.Latitude, request.Longitude);
        Result locationInCountry = await locationValidator.IsLocationInCountryAsync(geoPoint);

        if (!locationInCountry.IsSuccess)
        {
            return this.ErrorsResponse(locationInCountry.Errors);
        }

        DeliveryPoint entity = request.ToEntity();

        Result<DeliveryPoint> result = await Repository.CreateAsync(entity);

        return result.Match(entity => Ok(entity), this.ErrorsResponse);
    }
}
