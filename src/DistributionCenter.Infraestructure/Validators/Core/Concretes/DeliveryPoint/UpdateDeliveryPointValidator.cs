namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.DeliveryPoint;

using Bases;
using DTOs.Concretes.DeliveryPoint;
using Extensions;

public class UpdateDeliveryPointValidator: BaseFluentValidator<UpdateDeliveryPointDto>
{
    public UpdateDeliveryPointValidator()
    {
        _ = RuleFor(static x => x.Latitude)
            .WhenNotNull()
            .AddRule(latitude => latitude != 0, "Latitude cannot be zero.");

        _ = RuleFor(static x => x.Longitude)
            .WhenNotNull()
            .AddRule(longitude => longitude != 0, "Longitude cannot be zero.");
    }
}
