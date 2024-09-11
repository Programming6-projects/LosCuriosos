namespace DistributionCenter.Infraestructure.Validators.Core.Concretes.Clients;

using DistributionCenter.Infraestructure.DTOs.Concretes.Clients;
using DistributionCenter.Infraestructure.Validators.Core.Bases;
using DistributionCenter.Infraestructure.Validators.Extensions;

public class UpdateClientValidator : BaseFluentValidator<UpdateClientDto>
{
    public UpdateClientValidator()
    {
        _ = RuleFor(static x => x.Name)
            .WhenNotNull()
            .SizeRange(3, 50, "Name must be between 3 and 50 characters")
            .RegexValidator(@"^[a-zA-Z\s]+$", "Name must contain only letters and spaces");

        _ = RuleFor(static x => x.LastName)
            .WhenNotNull()
            .SizeRange(3, 50, "LastName must be between 3 and 50 characters")
            .RegexValidator(@"^[a-zA-Z\s]+$", "LastName must contain only letters and spaces");

        _ = RuleFor(static x => x.Email).WhenNotNull().EmailValidator("Email is not valid email");
    }
}
