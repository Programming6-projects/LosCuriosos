namespace DistributionCenter.Infraestructure.DTOs.Concretes.Transports;

using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Infraestructure.DTOs.Interfaces;
using DistributionCenter.Infraestructure.Validators.Core.Concretes.Transports;

public class CreateTransportDto : ICreateDto<Transport>
{
    public required string Name { get; init; }
    public required string Plate { get; init; }
    public required int Capacity { get; init; }
    public required int CurrentCapacity { get; init; }
    public required bool IsAvailable { get; init; }

    public Transport ToEntity()
    {
        return new Transport
        {
            Name = Name,
            Plate = Plate,
            Capacity = Capacity,
            CurrentCapacity = CurrentCapacity,
            IsAvailable = IsAvailable
        };
    }

    public Result Validate()
    {
        return new CreateTransportValidator().Validate(this);
    }
}
