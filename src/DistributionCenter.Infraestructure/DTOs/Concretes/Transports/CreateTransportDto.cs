namespace DistributionCenter.Infraestructure.DTOs.Concretes.Transports;

using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Infraestructure.DTOs.Interfaces;
using DistributionCenter.Infraestructure.Validators.Core.Concretes.Transports;

public class CreateTransportDto : ICreateDto<Transport>
{
    public required string Name { get; init; }
    public required int Capacity { get; init; }
    public required int AvailableUnits { get; init; }

    public Transport ToEntity()
    {
        return new Transport
        {
            Name = Name,
            Capacity = Capacity,
            AvailableUnits = AvailableUnits
        };
    }

    public Result Validate()
    {
        return new CreateTransportValidator().Validate(this);
    }
}
