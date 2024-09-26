namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.Orders;

using Bases;
using DTOs.Concretes.Orders;
using OrderProducts;

public class UpdateOrderValidator : BaseFluentValidator<UpdateOrderDto>
{
    public UpdateOrderValidator()
    {
        _ = RuleForEach(x => x.Products, new UpdateOrderProductValidator());
    }
}
