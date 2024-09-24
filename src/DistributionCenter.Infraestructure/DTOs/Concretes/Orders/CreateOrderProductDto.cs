namespace DistributionCenter.Infraestructure.DTOs.Concretes.Orders;

using Commons.Results;
using Domain.Entities.Concretes;
using Interfaces;
using Validators.Core.Concretes.Orders;

public class CreateOrderProductDto : ICreateDto<OrderProduct>
{
    public required Guid ProductId { get; set; }
    public required Guid OrderId { get; set; }
    public required int Quantity { get; set; }
    public required Product Product { get; set; }

    public OrderProduct ToEntity()
    {
        return new OrderProduct
        {
            ProductId = ProductId,
            OrderId = OrderId,
            Quantity = Quantity,
            Product = Product
        };
    }

    public Result Validate()
    {
        return new ClientOrderProductValidator().Validate(this);
    }
}
