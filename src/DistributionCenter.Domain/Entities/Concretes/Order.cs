namespace DistributionCenter.Domain.Entities.Concretes;

using Bases;
using Enums;

public class Order : BaseEntity
{
    public Status Status { get; set; } = Status.Pending;
    public required Guid RouteId { get; set; }
    public required Guid ClientId { get; set; }
    public required Guid DeliveryPointId { get; set; }
    public ICollection<OrderProduct> Products { get; init; } = [];
}
