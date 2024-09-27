namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.OrderProducts;

using Bases;
using DTOs.Concretes.OrderProducts;
using Extensions;

public class CreateOrderProductValidator : BaseFluentValidator<CreateOrderProductDto>
{
    public CreateOrderProductValidator()
    {
        _ = RuleFor(x => x.Quantity).NonNegatives("Quantity must be a non-negative integer");
    }
}
