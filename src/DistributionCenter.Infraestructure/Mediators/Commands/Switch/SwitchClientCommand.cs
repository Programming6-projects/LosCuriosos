namespace DistributionCenter.Infraestructure.Mediators.Commands.Switch;

using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Concretes;
using MediatR;

public class SwitchClientCommand : IRequest<Result<Client>>
{
    public required Guid Id { get; init; }
    public required bool IsActive { get; init; }
}
