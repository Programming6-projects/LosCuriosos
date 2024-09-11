namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.Products;

using Bases;
using DTOs.Concretes.Products;
using Extensions;

public class CreateProductValidator : BaseFluentValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        _ = RuleFor(static product => product.Name)!
            .WhenNotNull()
            .SizeRange(3, 64, "Name must be between 3 and 64 characters")
            .RegexValidator(
                @"^[a-zA-Z0-9\s\.-_]+$",
                "Name must contain letters, decimal and the follow characters: .,-_");

        _ = RuleFor(static product => product.Description)!
            .WhenNotNull()
            .SizeRange(3, 128, "The description has a limit of 128 characters");

        _ = RuleFor(static product => product.Weight)
            .DecimalSize(2, "The weight should contain only 2 decimals");
    }
}
