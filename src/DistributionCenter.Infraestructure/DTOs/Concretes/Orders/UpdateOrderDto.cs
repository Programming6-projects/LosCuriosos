namespace DistributionCenter.Infraestructure.DTOs.Concretes.Orders;

using Commons.Results;
using Domain.Entities.Concretes;
using Domain.Entities.Enums;
using Interfaces;
using OrderProducts;

public class UpdateOrderDto : IUpdateDto<Order>
{
    public Guid? RouteId { get; set; }
    public Guid? ClientId { get; set; }
    public Guid? DeliveryPointId { get; set; }
    public DateTime? DeliveryTime { get; set; }
    public string? Status { get; init; }
    public IEnumerable<UpdateOrderProductDto> Products { get; init; } = [];

    public Order FromEntity(Order entity)
    {
        if (Status is not null)
        {
            _ = Enum.TryParse(Status, out Status status);

            entity.RouteId = RouteId ?? entity.RouteId;
            entity.ClientId = ClientId ?? entity.ClientId;
            entity.DeliveryTime = DeliveryTime ?? entity.DeliveryTime;
            entity.DeliveryPointId = DeliveryPointId ?? entity.DeliveryPointId;
            entity.Status = status;
        }

        foreach (UpdateOrderProductDto product in Products)
        {
            if (entity.Products.All(p => p.ProductId != product.ProductId)) continue;
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
