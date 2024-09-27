namespace DistributionCenter.Infraestructure.DTOs.Concretes.OrderProducts;

using Commons.Results;
using Domain.Entities.Concretes;
using Interfaces;

public class UpdateOrderProductDto : IUpdateDto<OrderProduct>
{
    public Guid ProductId { get; set; }
    public int? Quantity { get; set; }

    public OrderProduct FromEntity(OrderProduct entity)
    {
        entity.ProductId = ProductId;
        entity.Quantity = Quantity ?? entity.Quantity;

        return entity;
    }

    public Result Validate()
    {
        return Result.Ok();
    }
}
