namespace DistributionCenter.Domain.Entities.Concretes;

using DistributionCenter.Domain.Entities.Bases;

public class Transport : BaseEntity
{
    public required string Name { get; set; }
    public required int Capacity { get; set; }
    public required int AvailableUnits { get; set; }
}
