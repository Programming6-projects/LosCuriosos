namespace DistributionCenter.Domain.Entities.Concretes;

using Bases;
using DistributionCenter.Domain.Entities.Enums;

public class Order : BaseEntity
{
    public Status Status { get; set; } = Status.Pending;
    public Guid? RouteId { get; set; }
    public required Guid ClientId { get; set; }
    public required Guid DeliveryPointId { get; set; }
    public required IReadOnlyList<OrderProduct> Products { get; set; }
}
