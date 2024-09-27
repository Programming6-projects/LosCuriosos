namespace DistributionCenter.Infraestructure.DTOs.Concretes.Orders;

using Commons.Results;
using Domain.Entities.Concretes;
using Domain.Entities.Enums;
using Interfaces;
using OrderProducts;

public class UpdateOrderDto : IUpdateDto<Order>
{
    public string? Status { get; init; }
    public IEnumerable<UpdateOrderProductDto> Products { get; init; } = [];

    public Order FromEntity(Order entity)
    {
        if (Status is not null)
        {
            _ = Enum.TryParse(Status, out Status status);

            entity.Status = status;
        }

        foreach (UpdateOrderProductDto product in Products)
        {
            if (!entity.Products.Any(p => p.ProductId == product.ProductId))
            {
                continue;
            }

            OrderProduct orderProduct = entity.Products.FirstOrDefault(p => p.ProductId == product.ProductId)!;

            orderProduct.Quantity = product.Quantity ?? orderProduct.Quantity;
        }

        return entity;
    }

    public Result Validate()
    {
        return Result.Ok();
    }
}
