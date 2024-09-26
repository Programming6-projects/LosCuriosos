namespace DistributionCenter.Domain.Entities.Concretes;

using Bases;
using Enums;

public class Order : BaseEntity
{
    public Guid RouteId { get; set; } = Guid.Empty;
    public required Guid ClientId { get; set; }
    public required Guid DeliveryPointId { get; set; }
    public IEnumerable<OrderProduct> Products { get; set; } = [];
    public Status Status { get; set; } = Status.Pending;

    public override bool Equals(object? obj)
    {
        if (obj is Order otherOrder)
        {
            return this.ClientId == otherOrder.ClientId && this.Status == otherOrder.Status;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ClientId, Status);
    }
}
