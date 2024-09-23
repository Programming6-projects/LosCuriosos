namespace DistributionCenter.Infraestructure.DTOs.Concretes.Trip;

using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Infraestructure.DTOs.Interfaces;
using DistributionCenter.Infraestructure.Validators.Core.Concretes.Trip;
using Domain.Entities.Enums;

public class CreateTripDto : ICreateDto<Trip>
{
    public required string Status { get; init; }
    public Guid? TransportId { get; init; }
    public Trip ToEntity()
    {
        _ = Enum.TryParse(Status, true, out Status parseStatus);
        return new Trip
        {
            Status = parseStatus,
            TransportId = TransportId
        };
    }

    public Result Validate()
    {
        return new CreateTripValidator().Validate(this);
    }
}
