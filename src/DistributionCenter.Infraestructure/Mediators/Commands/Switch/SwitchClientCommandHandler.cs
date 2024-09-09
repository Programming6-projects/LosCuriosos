// namespace DistributionCenter.Infraestructure.Mediators.Commands.Switch;
//
// using DistributionCenter.Application.Repositories.Interfaces;
// using DistributionCenter.Commons.Results;
// using DistributionCenter.Domain.Entities.Concretes;
// using MediatR;
//
// public class SwitchClientCommandHandler(IClientRepository repository)
//     : IRequestHandler<SwitchClientCommand, Result<Client>>
// {
//     private readonly IClientRepository _repository = repository;
//
//     public async Task<Result<Client>> Handle(SwitchClientCommand request, CancellationToken cancellationToken)
//     {
//         ArgumentNullException.ThrowIfNull(request, nameof(request));
//
//         Result<Client> clientResult = await _repository.GetByIdAsync(request.Id);
//
//         if (!clientResult.IsSuccess)
//         {
//             return clientResult.Errors;
//         }
//
//         Client client = clientResult.Value;
//         client.IsActive = request.IsActive;
//
//         return await _repository.UpdateAsync(client);
//     }
// }
