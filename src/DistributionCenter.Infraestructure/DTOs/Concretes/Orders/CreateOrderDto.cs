namespace DistributionCenter.Infraestructure.DTOs.Concretes.Orders;

using Commons.Results;
using Domain.Entities.Concretes;
using Interfaces;
using Validators.Core.Concretes.Orders;

public class CreateOrderDto : ICreateDto<Order>
{
    public required Guid ClientId { get; init; }
    public required Guid OrderStatusId { get; init; }

    public Order ToEntity()
    {
        return new Order
        {
            ClientId = ClientId,
            OrderStatusId = OrderStatusId,
        };
    }

    public Result Validate()
    {
        return new CreateOrderValidator().Validate(this);
    }
}
