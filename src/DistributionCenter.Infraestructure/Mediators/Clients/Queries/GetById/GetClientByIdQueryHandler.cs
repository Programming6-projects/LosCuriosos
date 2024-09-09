// namespace DistributionCenter.Infraestructure.Mediators.Clients.Queries.GetById;
//
// using DistributionCenter.Application.Repositories.Interfaces;
// using DistributionCenter.Commons.Results;
// using DistributionCenter.Domain.Entities.Concretes;
// using MediatR;
//
// public class GetClientByIdQueryHandler(IClientRepository clientRepository)
//     : IRequestHandler<GetClientByIdQuery, Result<Client>>
// {
//     private readonly IClientRepository _clientRepository = clientRepository;
//
//     public async Task<Result<Client>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
//     {
//         ArgumentNullException.ThrowIfNull(request, nameof(request));
//
//         return await _clientRepository.GetByIdAsync(request.Id);
//     }
// }
