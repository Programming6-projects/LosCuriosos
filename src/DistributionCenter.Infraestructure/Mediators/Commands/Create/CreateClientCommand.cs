namespace DistributionCenter.Infraestructure.Mediators.Commands.Create;

using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;
using MediatR;

public class CreateCommand<T, TDto> : IRequest<Result<T>>
    where T : IEntity
{
    public required TDto Dto { get; init; }
}
