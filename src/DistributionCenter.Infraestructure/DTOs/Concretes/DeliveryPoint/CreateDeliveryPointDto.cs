namespace DistributionCenter.Infraestructure.DTOs.Concretes.DeliveryPoint;

using Commons.Results;
using Domain.Entities.Concretes;
using Interfaces;
using Validators.Core.Concretes.DeliveryPoint;

public class CreateDeliveryPointDto : ICreateDto<DeliveryPoint>
{
    public required double Latitude { get; init; }
    public required double Longitude { get; init; }

    public DeliveryPoint ToEntity()
    {
        return new DeliveryPoint
        {
            Latitude = Latitude,
            Longitude = Longitude
        };
    }

    public Result Validate()
    {
        return new CreateDeliveryPointValidator().Validate(this);
    }
}
