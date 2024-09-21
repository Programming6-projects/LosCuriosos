namespace DistributionCenter.Infraestructure.DTOs.Concretes.Orders;

using Commons.Results;
using DistributionCenter.Domain.Entities.Enums;
using Domain.Entities.Concretes;
using Interfaces;
using Validators.Core.Concretes.Orders;

public class UpdateOrderDto : IUpdateDto<Order>
{
    public Status? Status { get; set; }
    public Guid? RouteId { get; set; }
    public Guid? ClientId { get; set; }
    public Guid? DeliveryPointId { get; set; }
    public IReadOnlyList<ClientOrderProduct>? ClientOrderProducts { get; set; }

    public Order FromEntity(Order entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        entity.Status = Status ?? entity.Status;
        entity.RouteId = RouteId ?? entity.RouteId;
        entity.ClientId = ClientId ?? entity.ClientId;
        entity.DeliveryPointId = DeliveryPointId ?? entity.DeliveryPointId;
        entity.ClientOrderProducts = ClientOrderProducts ?? entity.ClientOrderProducts;

        return entity;
    }

    public Result Validate()
    {
        return new UpdateOrderValidator().Validate(this);
    }
}
