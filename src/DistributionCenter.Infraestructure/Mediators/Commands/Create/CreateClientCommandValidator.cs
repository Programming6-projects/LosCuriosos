namespace DistributionCenter.Infraestructure.Mediators.Commands.Create;

using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Infraestructure.DTOs.Clients;
using DistributionCenter.Infraestructure.Validators.Clients;
using FluentValidation;

public class CreateCommandValidator : AbstractValidator<CreateCommand<Client, CreateClientDto>>
{
    public CreateCommandValidator()
    {
        _ = RuleFor(static x => x.Dto).NotNull().SetValidator(new CreateClientDtoValidator());
    }
}
