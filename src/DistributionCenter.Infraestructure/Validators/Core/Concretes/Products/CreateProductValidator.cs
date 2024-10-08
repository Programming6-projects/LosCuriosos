﻿namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.Products;

using Bases;
using DTOs.Concretes.Products;
using Extensions;

public class CreateProductValidator : BaseFluentValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        _ = RuleFor(static product => product.Name)!
            .NotNullNotEmpty("Name is required")
            .SizeRange(3, 64, "Name must be between 3 and 64 characters")
            .RegexValidator(
                @"^[a-zA-Z0-9\s\.]+$",
                "Name must contain letters, decimal and the follow characters: .,-_");

        _ = RuleFor(static product => product.Description)!
            .NotNullNotEmpty("Description is required")
            .SizeRange(3, 128, "The description has a limit of 128 characters");

        _ = RuleFor(static product => product.Weight)
            .NonNegatives("The weight can't be a negative number")
            .NumberRange(0, 1000000, "The weight has a limit of 1000000 gr/ml");
    }
}
