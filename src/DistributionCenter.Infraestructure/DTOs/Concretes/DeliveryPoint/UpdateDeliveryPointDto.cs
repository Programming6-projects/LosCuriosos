namespace DistributionCenter.Infraestructure.DTOs.Concretes.DeliveryPoint;

using Commons.Results;
using Domain.Entities.Concretes;
using Interfaces;
using Validators.Core.Concretes.DeliveryPoint;

public class UpdateDeliveryPointDto: IUpdateDto<DeliveryPoint>
{
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public DeliveryPoint FromEntity(DeliveryPoint entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        entity.Longitude = Longitude ?? entity.Longitude;
        entity.Latitude = Latitude ?? entity.Latitude;

        return entity;
    }

    public Result Validate()
    {
        return new UpdateDeliveryPointValidator().Validate(this);
    }
}
