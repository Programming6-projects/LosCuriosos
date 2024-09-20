namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.Orders;

using Bases;
using DTOs.Concretes.Orders;
using Extensions;

public class CreateOrderValidator : BaseFluentValidator<CreateOrderDto>
{
    public CreateOrderValidator()
    {
        _ = RuleFor<String>(static x => x.Status)
            .BelongsToStatus();

        _ = RuleFor<Guid>(static x => x.RouteId)
            .UuidNotNull("Route id is required");

        _ = RuleFor<Guid>(static x => x.DeliveryPointId)
            .UuidNotNull("Delivered Point id is required");

        _ = RuleForEach(x => x.ClientOrderProducts, new ClientOrderProductValidator());
    }
}
