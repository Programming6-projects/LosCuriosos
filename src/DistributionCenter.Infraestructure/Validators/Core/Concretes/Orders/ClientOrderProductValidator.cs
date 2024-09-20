namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.Orders;

using Bases;
using Domain.Entities.Concretes;
using Extensions;

public class ClientOrderProductValidator : BaseFluentValidator<ClientOrderProduct>
{
    public ClientOrderProductValidator()
    {
        _ = RuleFor(x => x.ProductId)
            .UuidNotNull("ProductId is required");

        _ = RuleFor(x => x.Quantity)
            .NonNegatives("Quantity must be a non-negative integer");
    }
}
