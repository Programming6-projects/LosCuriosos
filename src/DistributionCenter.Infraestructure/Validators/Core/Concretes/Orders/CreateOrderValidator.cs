namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.Orders;

using Bases;
using DTOs.Concretes.Orders;
using OrderProducts;

public class CreateOrderValidator : BaseFluentValidator<CreateOrderDto>
{
    public CreateOrderValidator()
    {
        _ = RuleForEach(x => x.Products, new CreateOrderProductValidator());
    }
}
