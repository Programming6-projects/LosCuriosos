namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.Strikes;

using Bases;
using DTOs.Concretes.Strikes;
using Extensions;

public class CreateStrikeValidator : BaseFluentValidator<CreateStrikeDto>
{
    public CreateStrikeValidator()
    {
        _ = RuleFor(static product => product.Description)
            .NotNullNotEmpty("The description can't be empty")
            .SizeRange(3, 128, "The description should have a length between 3 and 128 characters");

        _ = RuleFor(static product => product.TransportId).NotNullNotEmtpy("The transport id can't be empty");
    }
}
