namespace DistributionCenter.Infraestructure.DTOs.Concretes.Orders;

using Commons.Results;
using Domain.Entities.Concretes;
using Domain.Entities.Enums;
using Interfaces;
using Validators.Core.Concretes.Orders;

public class UpdateOrderDto: IUpdateDto<Order>
{
    public string? Status { get; init; }

    public Order FromEntity(Order entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        _ = Enum.TryParse(Status, true, out Status parseStatus);
        entity.Status = Status != null ? parseStatus : entity.Status;

        return entity;
    }

    public Result Validate()
    {
        return new UpdateOrderValidator().Validate(this);
    }
}
