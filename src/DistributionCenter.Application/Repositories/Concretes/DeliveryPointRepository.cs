namespace DistributionCenter.Application.Repositories.Concretes;

using Bases;
using Contexts.Interfaces;
using Domain.Entities.Concretes;
using Commons.Results;
using Services.Localization.Interfaces;
using Services.Localization.Commons;

public class DeliveryPointRepository(IContext context, ILocationValidator validator) : BaseRepository<DeliveryPoint>(context)
{
    private async Task<Result> ValidateLocation(GeoPoint geoPoint)
    {
        Result locationCountry = await validator.IsLocationInCountryAsync(geoPoint);

        if (!locationCountry.IsSuccess)
            return locationCountry.Errors;

        return Result.Ok();
    }

    public override async Task<Result<DeliveryPoint>> CreateAsync(DeliveryPoint entity)
    {
        Result locationCountry = await ValidateLocation(new(entity.Latitude, entity.Longitude));

        if (!locationCountry.IsSuccess)
            return locationCountry.Errors;

        return await base.CreateAsync(entity);
    }

    public override async Task<Result<DeliveryPoint>> UpdateAsync(DeliveryPoint entity)
    {
        Result locationCountry = await ValidateLocation(new(entity.Latitude, entity.Longitude));

        if (!locationCountry.IsSuccess)
            return locationCountry.Errors;

        return await base.UpdateAsync(entity);
    }
}
