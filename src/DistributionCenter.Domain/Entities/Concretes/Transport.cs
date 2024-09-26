namespace DistributionCenter.Domain.Entities.Concretes;

using DistributionCenter.Domain.Entities.Bases;

public class Transport : BaseEntity
{
    public required string Name { get; set; }
    public required string Plate { get; set; }
    public required int Capacity { get; set; }
    public required int CurrentCapacity { get; set; }
    public required bool IsAvailable { get; set; }
    public IEnumerable<Strike> Strikes { get; set; } = [];
}
