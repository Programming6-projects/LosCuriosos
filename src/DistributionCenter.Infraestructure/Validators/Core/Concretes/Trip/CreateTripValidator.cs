namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.Trip;

using DistributionCenter.Infraestructure.Validators.Core.Bases;
using DistributionCenter.Infraestructure.Validators.Extensions;
using DTOs.Concretes.Trip;

public class CreateTripValidator : BaseFluentValidator<CreateTripDto>
{
    public CreateTripValidator()
    {
        _ = RuleFor(static route => route.Status)
            .NotNullNotEmpty("The status is required")
            .BelongsToStatus();
    }
}
