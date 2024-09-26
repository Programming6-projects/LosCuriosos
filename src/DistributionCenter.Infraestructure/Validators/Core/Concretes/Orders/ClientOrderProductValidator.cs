namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.Orders;

using Bases;
using DTOs.Concretes.Orders;
using Extensions;

public class ClientOrderProductValidator : BaseFluentValidator<CreateOrderProductDto>
{
    public ClientOrderProductValidator()
    {
        _ = RuleFor(x => x.ProductId)
            .UuidNotNull("ProductId is required");

        _ = RuleFor(x => x.OrderId)
            .UuidNotNull("OrderId is required");

        _ = RuleFor(x => x.Quantity)
            .NonNegatives("Quantity must be a non-negative integer");
    }
}