namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.Orders;

using Bases;
using DistributionCenter.Infraestructure.Validators.Extensions;
using DTOs.Concretes.Orders;

public class CreateOrderValidator : BaseFluentValidator<CreateOrderDto>
{
    public CreateOrderValidator()
    {
        _ = RuleFor<Guid>(static x => x.ClientId)
            .UuidNotNull("Client id is required");

        _ = RuleFor<Guid>(static x => x.DeliveryPointId)
            .UuidNotNull("Delivered Point id is required");

        _ = RuleForEach(x => x.OrderProducts, new ClientOrderRequestProductValidator());
    }
}
