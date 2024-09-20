namespace DistributionCenter.Infraestructure.DTOs.Concretes.Orders;

using Commons.Results;
using Domain.Entities.Concretes;
using Interfaces;
using Validators.Core.Concretes.Orders;

public class CreateOrderDto : ICreateDto<ClientOrder>
{
    public required string Status { get; set; }
    public required Guid RouteId { get; set; }
    public required Guid ClientId { get; set; }
    public required Guid DeliveryPointId { get; set; }
    public required IReadOnlyList<ClientOrderProduct> ClientOrderProducts { get; set; }

    public ClientOrder ToEntity()
    {
        _ = Enum.TryParse(Status, true, out Commons.Enums.Status parseStatus);
        return new ClientOrder
        {
            ClientId = ClientId,
            Status = parseStatus,
            RouteId = RouteId,
            DeliveryPointId = DeliveryPointId,
            ClientOrderProducts = ClientOrderProducts
        };
    }

    public Result Validate()
    {
        return new CreateOrderValidator().Validate(this);
    }
}
