// namespace DistributionCenter.Infraestructure.Mediators.Commands.Update;
//
// using AutoMapper;
// using DistributionCenter.Application.Repositories.Interfaces;
// using DistributionCenter.Commons.Results;
// using DistributionCenter.Domain.Entities.Concretes;
// using MediatR;
//
// public class UpdateClientCommandHandler(IClientRepository clientRepository, IMapper mapper)
//     : IRequestHandler<UpdateClientCommand, Result<Client>>
// {
//     private readonly IClientRepository _clientRepository = clientRepository;
//     private readonly IMapper _mapper = mapper;
//
//     public async Task<Result<Client>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
//     {
//         ArgumentNullException.ThrowIfNull(request, nameof(request));
//
//         Result<Client> client = await _clientRepository.GetByIdAsync(request.Id);
//
//         if (!client.IsSuccess)
//         {
//             return client.Errors;
//         }
//
//         Client clientToUpdate = _mapper.Map(request.ClientDto, client.Value);
//
//         return await _clientRepository.UpdateAsync(clientToUpdate);
//     }
// }
