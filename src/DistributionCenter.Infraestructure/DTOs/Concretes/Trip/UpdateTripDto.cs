namespace DistributionCenter.Infraestructure.DTOs.Concretes.Trip;

using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Infraestructure.DTOs.Interfaces;
using Domain.Entities.Enums;
using Validators.Core.Concretes.Trip;

public class UpdateTripDto : IUpdateDto<Trip>
{
    public string? Status { get; init; }
    public Guid? TransportId { get; init; }
    public Trip FromEntity(Trip entity)
    {
        _ = Enum.TryParse(Status, true, out Status parseStatus);
        entity.Status = Status != null ? parseStatus : entity.Status;
        entity.TransportId = TransportId ?? entity.TransportId;
        return entity;
    }

    public Result Validate()
    {
        return new UpdateTripValidator().Validate(this);
    }
}
