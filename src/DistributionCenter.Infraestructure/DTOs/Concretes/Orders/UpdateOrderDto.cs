namespace DistributionCenter.Infraestructure.DTOs.Concretes.Orders;

using Commons.Results;
using Domain.Entities.Concretes;
using Interfaces;
using Validators.Core.Concretes.Orders;

public class UpdateOrderDto: IUpdateDto<Order>
{
    public Guid? OrderStatusId { get; init; }

    public Order FromEntity(Order client)
    {
        ArgumentNullException.ThrowIfNull(client, nameof(client));

        client.OrderStatusId = OrderStatusId ?? client.OrderStatusId;

        return client;
    }

    public Result Validate()
    {
        return new UpdateOrderValidator().Validate(this);
    }
}
