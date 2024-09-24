namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.Trip;

using DistributionCenter.Infraestructure.DTOs.Concretes.Trip;
using DistributionCenter.Infraestructure.Validators.Core.Bases;
using DistributionCenter.Infraestructure.Validators.Extensions;

public class UpdateTripValidator : BaseFluentValidator<UpdateTripDto>
{
    public UpdateTripValidator()
    {
        _ = RuleFor(static route => route.Status)!
            .BelongsToStatus();
    }
}
