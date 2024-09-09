namespace DistributionCenter.Infraestructure.Validators.Clients;

using DistributionCenter.Infraestructure.DTOs.Clients;
using FluentValidation;

public class CreateClientDtoValidator : AbstractValidator<CreateClientDto>
{
    public CreateClientDtoValidator()
    {
        _ = RuleFor(static x => x.Name).NotNullNotEmpty("Name").SizeRange("Name", 3, 50);
        _ = RuleFor(static x => x.LastName).NotNullNotEmpty("Last Name").SizeRange("Last Name", 3, 50);
        _ = RuleFor(static x => x.Email).NotNullNotEmpty("Email").EmailAddress().WithMessage("Email is invalid");
    }
}
