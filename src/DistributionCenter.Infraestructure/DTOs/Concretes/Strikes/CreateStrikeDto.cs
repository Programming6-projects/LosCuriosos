namespace DistributionCenter.Infraestructure.DTOs.Concretes.Strikes;

using Commons.Results;
using Domain.Entities.Concretes;
using Interfaces;
using Validators.Core.Concretes.Strikes;

public class CreateStrikeDto : ICreateDto<Strike>
{
    public required string Description { get; set; }
    public required Guid TransportId { get; set; }

    public Strike ToEntity()
    {
        return new Strike { Description = Description, TransportId = TransportId };
    }

    public Result Validate()
    {
        return new CreateStrikeValidator().Validate(this);
    }
}
