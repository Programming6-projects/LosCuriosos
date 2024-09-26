namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.Orders;

using Bases;
using DTOs.Concretes.Orders;
using Extensions;

public class UpdateOrderProductValidator: BaseFluentValidator<UpdateOrderProductDto>
{
    public UpdateOrderProductValidator()
    {
        _ = RuleFor(x => x.Quantity)
            .NonNegatives("Quantity must be a non-negative integer");
    }
}
