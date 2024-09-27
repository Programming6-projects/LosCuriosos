namespace DistributionCenter.Domain.Entities.Concretes;

using Bases;

public class Strike : BaseEntity
{
    public required string Description { get; set; }
    public required Guid TransportId { get; set; }
}
