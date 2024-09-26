namespace DistributionCenter.Infraestructure.DTOs.Concretes.Orders;

using Commons.Results;
using Domain.Entities.Concretes;
using Interfaces;
using Validators.Core.Concretes.Orders;

public class UpdateOrderDto : IUpdateDto<Order>
{
    public Status? Status { get; set; }
    public IReadOnlyList<UpdateOrderProductDto>? OrderProducts { get; set; }

    public Order FromEntity(Order entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        entity.Status = Status ?? entity.Status;
        return entity;
    }

    public Result Validate()
    {
        return new UpdateOrderValidator().Validate(this);
    }
}
