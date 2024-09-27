namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.OrderProducts;

using Bases;
using DTOs.Concretes.OrderProducts;
using Extensions;

public class UpdateOrderProductValidator : BaseFluentValidator<UpdateOrderProductDto>
{
    public UpdateOrderProductValidator()
    {
        _ = RuleFor(x => x.Quantity).WhenNotNull().NonNegatives("Quantity must be a non-negative integer");
    }
}
