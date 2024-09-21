namespace DistributionCenter.Domain.Entities.Concretes;

using Bases;

public class OrderProduct : BaseEntity
{
    public required Guid ProductId { get; set; }
    public required Guid OrderId { get; set; }
    public required int Quantity { get; set; }
    public required Product Product { get; set; }
}
