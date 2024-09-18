namespace DistributionCenter.Infraestructure.DTOs.Concretes.Trip;

using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Infraestructure.DTOs.Interfaces;
using DistributionCenter.Infraestructure.Validators.Core.Concretes.Trip;

public class CreateTripDto : ICreateDto<Trip>
{
    public required string Status { get; init; }
    public required Guid? TransportId { get; init; }
    public Trip ToEntity()
    {
        _ = Enum.TryParse(Status, true, out Commons.Enums.Status parseStatus);
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
