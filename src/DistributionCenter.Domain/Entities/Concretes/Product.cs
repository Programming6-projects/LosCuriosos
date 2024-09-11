namespace DistributionCenter.Domain.Entities.Concretes;

using Bases;

public class Product : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required double Weight { get; set; }
}
