namespace DistributionCenter.Domain.Entities.Concretes;

using Bases;
using Enums;

public class Order : BaseEntity
{
    public required Guid ClientId { get; set; }
    public required Status Status { get; set; } = Status.Pending;

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
