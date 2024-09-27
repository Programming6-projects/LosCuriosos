namespace DistributionCenter.Infraestructure.DTOs.Concretes.Orders;

using Commons.Results;
using Domain.Entities.Concretes;
using Domain.Entities.Enums;
using Interfaces;
using Validators.Core.Concretes.Orders;

public class UpdateOrderDto : IUpdateDto<Order>
{
    public Guid? RouteId { get; set; }
    public Guid? ClientId { get; set; }
    public Guid? DeliveryPointId { get; set; }
    public DateTime? DeliveryTime { get; set; }

    public string? Status { get; init; }

    public Order FromEntity(Order entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        entity.RouteId = RouteId ?? entity.RouteId;
        entity.ClientId = ClientId ?? entity.ClientId;
        entity.DeliveryTime = DeliveryTime ?? entity.DeliveryTime;
        entity.DeliveryPointId = DeliveryPointId ?? entity.DeliveryPointId;

        _ = Enum.TryParse(Status, true, out Status parseStatus);
        entity.Status = Status != null ? parseStatus : entity.Status;

        return entity;
    }

    public Result Validate()
    {
        return new UpdateOrderValidator().Validate(this);
    }
}
