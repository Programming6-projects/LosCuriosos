namespace DistributionCenter.Infraestructure.DTOs.Concretes.Orders;

using Commons.Results;
using Domain.Entities.Concretes;
using Interfaces;

public class CreateOrderProductDto : ICreateDto<OrderProduct>
{
    public required Guid ProductId { get; set; }
    public required int Quantity { get; set; }

    public OrderProduct ToEntity()
    {
        return new OrderProduct
        {
            ProductId = ProductId,
            Quantity = Quantity
        };
    }

    public Result Validate()
    {
        return Result.Ok();
    }
}
