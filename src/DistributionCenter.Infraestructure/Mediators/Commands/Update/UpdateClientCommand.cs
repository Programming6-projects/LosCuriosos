namespace DistributionCenter.Infraestructure.Mediators.Commands.Update;

using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Infraestructure.DTOs.Clients;
using MediatR;

public class UpdateClientCommand : IRequest<Result<Client>>
{
    public required Guid Id { get; init; }

    public required UpdateClientDto ClientDto { get; init; }
}
