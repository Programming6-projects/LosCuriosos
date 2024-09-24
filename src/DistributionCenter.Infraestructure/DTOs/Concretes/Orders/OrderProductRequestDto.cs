namespace DistributionCenter.Infraestructure.DTOs.Concretes.Orders;

public class OrderProductRequestDto
{
    public required Guid ProductId { get; set; }
    public required int Quantity { get; set; }
}
