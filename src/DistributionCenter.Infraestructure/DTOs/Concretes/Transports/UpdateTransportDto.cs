namespace DistributionCenter.Infraestructure.DTOs.Concretes.Transports;

using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Infraestructure.DTOs.Interfaces;
using DistributionCenter.Infraestructure.Validators.Core.Concretes.Transports;

public class UpdateTransportDto : IUpdateDto<Transport>
{
    public required string? Name { get; init; }
    public required int? Capacity { get; init; }
    public required int? AvailableUnits { get; init; }

    public Transport FromEntity(Transport entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        entity.Name = Name ?? entity.Name;
        entity.Capacity = Capacity ?? entity.Capacity;
        entity.AvailableUnits = AvailableUnits ?? entity.AvailableUnits;

        return entity;
    }

    public Result Validate()
    {
        return new UpdateTransportValidator().Validate(this);
    }
}
