namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.Orders;

using Bases;
using DTOs.Concretes.Orders;
using Extensions;

public class CreateOrderValidator : BaseFluentValidator<CreateOrderDto>
{
    public CreateOrderValidator()
    {
        _ = RuleFor<Guid>(static x => x.ClientId)
            .UuidNotNull("Client id is required");

        _ = RuleFor<Guid>(static x => x.OrderStatusId)
            .UuidNotNull("Order status id is required");
    }
}
