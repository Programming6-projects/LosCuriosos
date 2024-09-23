namespace DistributionCenter.Domain.Entities.Concretes;

using Bases;
using Enums;

public class Trip : BaseEntity
{
    public required Status Status { get; set; } = Status.Pending;
    public required Guid? TransportId { get; set; }
    public ICollection<Order> Orders { get; init; } = [];
}
