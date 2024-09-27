namespace DistributionCenter.Infraestructure.DTOs.Concretes.Orders;

using Commons.Results;
using Domain.Entities.Concretes;
using Domain.Entities.Enums;
using Interfaces;
using Validators.Core.Concretes.Orders;

public class CreateOrderDto : ICreateDto<Order>
{
    public required Guid RouteId { get; init; }
    public required Guid ClientId { get; init; }
    public required Guid DeliveryPointId { get; init; }
    public required DateTime DeliveryTime { get; set; }
    public required string Status { get; init; }

    public Order ToEntity()
    {
        _ = Enum.TryParse(Status, true, out Status parseStatus);
        return new Order
        {
            RouteId = RouteId,
            ClientId = ClientId,
            Status = parseStatus,
            DeliveryTime = DeliveryTime,
            DeliveryPointId = DeliveryPointId,
        };
    }

    public Result Validate()
    {
        return new CreateOrderValidator().Validate(this);
    }
}
