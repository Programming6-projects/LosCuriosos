namespace DistributionCenter.Infraestructure.DTOs.Concretes.Trip;

using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Infraestructure.DTOs.Interfaces;
using Validators.Core.Concretes.Trip;

public class UpdateTripDto : IUpdateDto<Trip>
{
    public required string? Status { get; init; }
    public required Guid? TransportId { get; init; }
    public Trip FromEntity(Trip entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        Commons.Enums.Status parseStatus = Commons.Enums.Status.Cancelled;
        bool isParsed = false;
        if (Status != null)
        {
            isParsed = Enum.TryParse(Status, true, out parseStatus);
        }
        return new Trip
        {
            Status = isParsed ? parseStatus : entity.Status,
            TransportId = TransportId ?? entity.TransportId,
        };
    }

    public Result Validate()
    {
        return new UpdateTripValidator().Validate(this);
    }
}
