namespace DistributionCenter.Infraestructure.DTOs.Concretes.Orders;

using Commons.Results;
using DistributionCenter.Domain.Entities.Enums;
using Domain.Entities.Concretes;
using Interfaces;
using Validators.Core.Concretes.Orders;

public class CreateOrderDto : ICreateDto<Order>
{
    public required string Status { get; set; }
    public required Guid RouteId { get; set; }
    public required Guid ClientId { get; set; }
    public required Guid DeliveryPointId { get; set; }
    public required IReadOnlyList<OrderProduct> ClientOrderProducts { get; set; }

    public Order ToEntity()
    {
        _ = Enum.TryParse(Status, true, out Status parseStatus);
        return new Order
        {
            RouteId = RouteId,
            ClientId = ClientId,
            Status = parseStatus,
            DeliveryPointId = DeliveryPointId,
            Products = ClientOrderProducts
        };
    }

    public Result Validate()
    {
        return new CreateOrderValidator().Validate(this);
    }
}
