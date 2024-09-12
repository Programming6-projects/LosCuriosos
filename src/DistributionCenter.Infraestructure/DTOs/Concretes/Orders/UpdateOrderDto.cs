namespace DistributionCenter.Infraestructure.DTOs.Concretes.Orders;

using Commons.Results;
using Domain.Entities.Concretes;
using Interfaces;
using Validators.Core.Concretes.Orders;

public class UpdateOrderDto: IUpdateDto<Order>
{
    public Guid? ClientId { get; init; }
    public Guid? OrderStatusId { get; init; }

    public Order FromEntity(Order client)
    {
        ArgumentNullException.ThrowIfNull(client, nameof(client));

        client.ClientId = ClientId ?? client.ClientId;
        client.OrderStatusId = OrderStatusId ?? client.OrderStatusId;

        return client;
    }

    public Result Validate()
    {
        return new UpdateOrderValidator().Validate(this);
    }
}
