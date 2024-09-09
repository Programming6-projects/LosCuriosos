namespace DistributionCenter.Infraestructure.Mediators.Commands.Update;

using DistributionCenter.Infraestructure.Validators.Clients;
using FluentValidation;

public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
{
    public UpdateClientCommandValidator()
    {
        _ = RuleFor(static x => x.ClientDto).NotNull().SetValidator(new UpdateClientDtoValidator());
    }
}
