namespace DistributionCenter.Infraestructure.Validators.Clients;

using DistributionCenter.Infraestructure.DTOs.Clients;
using FluentValidation;

public class UpdateClientDtoValidator : AbstractValidator<UpdateClientDto>
{
    public UpdateClientDtoValidator()
    {
        _ = RuleFor(static x => x.Name)
            .NotNullNotEmpty("Name")
            .SizeRange("Name", 3, 50)
            .When(static x => x.Name is not null);

        _ = RuleFor(static x => x.LastName)
            .NotNullNotEmpty("Last Name")
            .SizeRange("Last Name", 3, 50)
            .When(static x => x.LastName is not null);

        _ = RuleFor(static x => x.Email)
            .NotNullNotEmpty("Email")
            .EmailAddress()
            .WithMessage("Email is invalid")
            .When(static x => x.Email is not null);
    }
}
