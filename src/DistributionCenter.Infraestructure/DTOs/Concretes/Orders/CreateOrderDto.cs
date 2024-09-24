namespace DistributionCenter.Infraestructure.DTOs.Concretes.Orders;

using Commons.Results;
using Domain.Entities.Concretes;
using Interfaces;
using Validators.Core.Concretes.Orders;

public class CreateOrderDto : ICreateDto<Order>
{
    public required Guid ClientId { get; set; }
    public required Guid DeliveryPointId { get; set; }
    public required IReadOnlyList<OrderProductRequestDto> OrderProducts { get; set; }

    public Order ToEntity()
    {
        IReadOnlyList<OrderProduct> products = new List<OrderProduct>();
        Order order = new()
        {
            RouteId = null,
            ClientId = ClientId,
            DeliveryPointId = DeliveryPointId,
            Products = products
        };
        return order;
    }

    public Result Validate()
    {
        return new CreateOrderValidator().Validate(this);
    }
}
