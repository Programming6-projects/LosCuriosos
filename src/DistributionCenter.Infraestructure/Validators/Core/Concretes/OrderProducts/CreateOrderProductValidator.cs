namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.Orders;

using Bases;
using DTOs.Concretes.Orders;
using Extensions;

public class CreateOrderProductValidator : BaseFluentValidator<CreateOrderProductDto>
{
    public CreateOrderProductValidator()
    {
        _ = RuleFor(x => x.Quantity)
            .NonNegatives("Quantity must be a non-negative integer");
    }
}
