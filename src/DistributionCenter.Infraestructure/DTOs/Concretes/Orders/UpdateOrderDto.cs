namespace DistributionCenter.Infraestructure.DTOs.Concretes.Orders;

using Commons.Enums;
using Commons.Results;
using Domain.Entities.Concretes;
using Interfaces;
using Validators.Core.Concretes.Orders;

public class UpdateOrderDto: IUpdateDto<ClientOrder>
{
    public Status? Status { get; set; }
    public Guid? RouteId { get; set; }
    public Guid? ClientId { get; set; }
    public Guid? DeliveryPointId { get; set; }
    public IReadOnlyList<ClientOrderProduct>? ClientOrderProducts { get; set; }

    public ClientOrder FromEntity(ClientOrder entity)
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
