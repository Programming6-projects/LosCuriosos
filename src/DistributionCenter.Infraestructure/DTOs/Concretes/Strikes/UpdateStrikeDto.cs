namespace DistributionCenter.Infraestructure.DTOs.Concretes.Strikes;

using Commons.Results;
using Domain.Entities.Concretes;
using Interfaces;
using Validators.Core.Concretes.Strikes;

public class UpdateStrikeDto : IUpdateDto<Strike>
{
    public string? Description { get; init; }
    public Guid? TransportId { get; init; }

    public Strike FromEntity(Strike entity)
    {
        entity.Description = Description ?? entity.Description;
        entity.TransportId = TransportId ?? entity.TransportId;

        return entity;
    }

    public Result Validate()
    {
        return new UpdateStrikeValidator().Validate(this);
    }
}
