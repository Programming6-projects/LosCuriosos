namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.DeliveryPoint;

using Bases;
using DTOs.Concretes.DeliveryPoint;
using Extensions;

public class CreateDeliveryPointValidator : BaseFluentValidator<CreateDeliveryPointDto>
{
    public CreateDeliveryPointValidator()
    {
        _ = RuleFor(static x => x.Latitude)
            .WhenNotEmpty()
            .AddRule(latitude => latitude != 0, "Latitude cannot be zero.");

        _ = RuleFor(static x => x.Longitude)
            .WhenNotEmpty()
            .AddRule(longitude => longitude != 0, "Longitude cannot be zero.");
    }
}
