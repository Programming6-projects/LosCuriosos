namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.Transports;

using Bases;
using DTOs.Concretes.Transports;
using Extensions;

public class CreateTransportValidator : BaseFluentValidator<CreateTransportDto>
{
    public CreateTransportValidator()
    {
        _ = RuleFor(static transport => transport.Name)!
            .WhenNotNull()
            .SizeRange(1, 20, "Name must be between 1 and 20 characters")
            .RegexValidator(
                @"^[a-zA-Z0-9\s]+$",
                "Name must contain letters, numbers and spaces");

        _ = RuleFor(static transport => transport.Plate)!
            .WhenNotNull()
            .SizeRange(4, 7, "Plate must be between 4 and 7 characters")
            .RegexValidator(
                @"^[0-9]{1,4}[A-Z]{3}$",
                    "Plate must contain 1 to 4 numbers and 3 uppercase letters");

        _ = RuleFor(static transport => transport.Capacity)
            .NonNegatives("The capacity cannot be a negative number")
            .NumberRange(500000, 450000000, "Capacity must be between 500.000 and 450.000.000 grams");

        _ = RuleFor(static transport => transport.CurrentCapacity)
            .NonNegatives("Available units cannot be a negative number")
            .NumberRange(500000, 450000000, "Current Capacity must be between 500.000 and 450.000.000 grams");
    }
}
