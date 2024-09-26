namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.Orders;

using Bases;
using DTOs.Concretes.Orders;

public class UpdateOrderValidator : BaseFluentValidator<UpdateOrderDto>
{
    public UpdateOrderValidator()
    {
        _ = RuleForEach(x => x.Products, new UpdateOrderProductValidator());
    }
}
