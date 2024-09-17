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
            .SizeRange(1, 64, "Name must be between 3 and 64 characters")
            .RegexValidator(
                @"^[a-zA-Z0-9\s]+$",
                "Name must contain letters, numbers and spaces");

        _ = RuleFor(static transport => transport.Capacity)
            .NonNegatives("The capacity cannot be a negative number")
            .NumberRange(1, 2000000, "Capacity must be between 1 and 2,000,000 grams");

        _ = RuleFor(static transport => transport.AvailableUnits)
            .NonNegatives("Available units cannot be a negative number")
            .NumberRange(1, 1000, "Capacity must be between 1 and 1000 units");
    }
}
