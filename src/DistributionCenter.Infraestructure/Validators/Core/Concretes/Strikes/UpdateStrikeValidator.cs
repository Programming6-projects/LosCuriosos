namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.Strikes;

using Bases;
using DTOs.Concretes.Strikes;
using Extensions;

public class UpdateStrikeValidator : BaseFluentValidator<UpdateStrikeDto>
{
    public UpdateStrikeValidator()
    {
        _ = RuleFor(static product => product.Description)
            .WhenNotNull()
            .NotNullNotEmpty("The description can't be empty")
            .SizeRange(3, 128, "The description has a limit of 128 characters");
    }
}
