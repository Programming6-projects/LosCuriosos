namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.DeliveryPoint;

using Bases;
using DTOs.Concretes.DeliveryPoint;

public class CreateDeliveryPointValidator : BaseFluentValidator<CreateDeliveryPointDto>
{
    public CreateDeliveryPointValidator()
    {
        _ = RuleFor(static x => x.Latitude)
            .AddRule(latitude => latitude != 0, "Latitude cannot be zero.");

        _ = RuleFor(static x => x.Longitude)
            .AddRule(longitude => longitude != 0, "Longitude cannot be zero.");
    }
}
