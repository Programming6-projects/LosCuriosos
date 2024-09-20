namespace DistributionCenter.Infraestructure.DTOs.Concretes.Orders;

using Commons.Results;
using Domain.Entities.Concretes;
using Interfaces;
using Validators.Core.Concretes.Orders;

public class UpdateOrderDto : IUpdateDto<Order>
{
    public Guid? RouteId { get; set; }
    public Guid? ClientId { get; set; }
    public Guid? DeliveryPointId { get; set; }

    public Order FromEntity(Order entity)
    {
        entity.RouteId = RouteId ?? entity.RouteId;
        entity.ClientId = ClientId ?? entity.ClientId;
        entity.DeliveryPointId = DeliveryPointId ?? entity.DeliveryPointId;

        return entity;
    }

    public Result Validate()
    {
        return new UpdateOrderValidator().Validate(this);
    }
}
