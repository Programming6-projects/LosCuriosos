namespace DistributionCenter.Infraestructure.Mediators.Commands.Create;

using AutoMapper;
using DistributionCenter.Application.Repositories.Interfaces;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;
using MediatR;

public class CreateCommandHandler<T, TDto>(IRepository<T> repository, IMapper mapper)
    : IRequestHandler<CreateCommand<T, TDto>, Result<T>>
    where T : IEntity
{
    private readonly IRepository<T> _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<T>> Handle(CreateCommand<T, TDto> request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        T entity = _mapper.Map<T>(request.Dto);

        return await _repository.CreateAsync(entity);
    }
}
