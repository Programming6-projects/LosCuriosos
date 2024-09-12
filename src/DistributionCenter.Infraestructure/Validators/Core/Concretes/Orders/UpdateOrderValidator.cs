namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.Orders;

using Bases;
using DTOs.Concretes.Orders;
using Extensions;

public class UpdateOrderValidator : BaseFluentValidator<UpdateOrderDto>
{
    public UpdateOrderValidator()
    {
        _ = RuleFor(static x => x.ClientId)
            .UuidNotNull("Client id is required");

        _ = RuleFor(static x => x.OrderStatusId)
            .UuidNotNull("Order status id is required");
    }
}
