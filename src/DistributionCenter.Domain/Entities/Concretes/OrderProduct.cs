namespace DistributionCenter.Domain.Entities.Concretes;

using Bases;

public class OrderProduct : BaseEntity
{
    public required Guid ProductId { get; set; }
    public Guid OrderId { get; set; } = Guid.Empty;
    public required int Quantity { get; set; }
    public Product Product { get; set; } = null!;
}
