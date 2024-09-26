namespace DistributionCenter.Infraestructure.DTOs.Concretes.Orders;

using Commons.Results;
using Domain.Entities.Concretes;
using Interfaces;
using Validators.Core.Concretes.Orders;

public class UpdateOrderProductDto: IUpdateDto<OrderProduct>
{
    public required Guid OrderProductId { get; set; }
    public Guid ProductId { get; set; }
    public int? Quantity { get; set; }

    public OrderProduct FromEntity(OrderProduct entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        entity.ProductId = ProductId;
        entity.Quantity = Quantity ?? entity.Quantity;

        return entity;
    }

    public Result Validate()
    {
        return new UpdateOrderProductValidator().Validate(this);
    }
}
