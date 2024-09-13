namespace DistributionCenter.Infraestructure.DTOs.Concretes.Orders;

using Commons.Results;
using Domain.Entities.Concretes;
using Interfaces;
using Validators.Core.Concretes.Orders;

public class UpdateOrderDto: IUpdateDto<Order>
{
    public Guid? OrderStatusId { get; init; }

    public Order FromEntity(Order entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        entity.OrderStatusId = OrderStatusId ?? entity.OrderStatusId;

        return entity;
    }

    public Result Validate()
    {
        return new UpdateOrderValidator().Validate(this);
    }
}
