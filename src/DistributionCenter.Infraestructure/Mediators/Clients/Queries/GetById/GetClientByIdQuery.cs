namespace DistributionCenter.Infraestructure.Mediators.Clients.Queries.GetById;

using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Concretes;
using MediatR;

public class GetClientByIdQuery : IRequest<Result<Client>>
{
    public required Guid Id { get; init; }
}
