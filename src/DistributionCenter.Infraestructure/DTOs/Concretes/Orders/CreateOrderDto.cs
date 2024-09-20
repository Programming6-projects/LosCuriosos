namespace DistributionCenter.Infraestructure.DTOs.Concretes.Orders;

using Commons.Results;
using Domain.Entities.Concretes;
using Domain.Entities.Enums;
using Interfaces;
using Validators.Core.Concretes.Orders;

public class CreateOrderDto : ICreateDto<Order>
{
    public required Guid ClientId { get; init; }
    public required string Status { get; init; }

    public Order ToEntity()
    {
        _ = Enum.TryParse(Status, true, out Status parseStatus);
        return new Order
        {
            ClientId = ClientId,
            Status = parseStatus,
        };
    }

    public Result Validate()
    {
        return new CreateOrderValidator().Validate(this);
    }
}
