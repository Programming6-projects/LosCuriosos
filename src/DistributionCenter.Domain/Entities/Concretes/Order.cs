namespace DistributionCenter.Domain.Entities.Concretes;

using Bases;

public class Order : BaseEntity
{
    public required Guid ClientId { get; set; }
    public required Guid OrderStatusId { get; set; }
}
