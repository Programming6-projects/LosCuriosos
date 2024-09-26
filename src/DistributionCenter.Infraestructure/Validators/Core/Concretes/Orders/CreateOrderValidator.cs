namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.Orders;

using Bases;
using DTOs.Concretes.Orders;

public class CreateOrderValidator : BaseFluentValidator<CreateOrderDto>
{
    public CreateOrderValidator()
    {
        _ = RuleForEach(x => x.OrderProducts, new ClientOrderRequestProductValidator());
    }
}
