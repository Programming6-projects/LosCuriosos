namespace DistributionCenter.Infraestructure.DTOs.Concretes.Orders;

using Commons.Results;
using Domain.Entities.Concretes;
using Interfaces;
using Validators.Core.Concretes.Orders;

public class CreateOrderDto : ICreateDto<Order>
{
    public required Guid ClientId { get; init; }
    public required Guid DeliveryPointId { get; init; }
    public required IEnumerable<CreateOrderProductDto> Products { get; init; }

    public Order ToEntity()
    {

        Order order = new()
        {
            ClientId = ClientId,
            DeliveryPointId = DeliveryPointId
        };

        List<OrderProduct> products = [];

        foreach (CreateOrderProductDto product in Products)
        {
            OrderProduct orderProduct = new()
            {
                OrderId = order.Id,
                ProductId = product.ProductId,
                Quantity = product.Quantity
            };

            products.Add(orderProduct);
        }

        order.Products = products;

        return order;
    }

    public Result Validate()
    {
        return new CreateOrderValidator().Validate(this);
    }
}
