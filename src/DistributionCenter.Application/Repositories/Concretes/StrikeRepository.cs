namespace DistributionCenter.Application.Repositories.Concretes;

using Bases;
using Contexts.Interfaces;
using DistributionCenter.Commons.Results;
using Domain.Entities.Concretes;

public class StrikeRepository(IContext context) : BaseRepository<Strike>(context)
{
    public override async Task<Result<Strike>> CreateAsync(Strike entity)
    {
        Result<Transport> resultTransport = await Context
            .SetTable<Transport>()
            .GetById(entity.TransportId)
            .ExecuteAsync();

        if (!resultTransport.IsSuccess)
        {
            return resultTransport.Errors;
        }

        return await base.CreateAsync(entity);
    }

    public override async Task<Result<Strike>> UpdateAsync(Strike entity)
    {
        Result<Transport> resultTransport = await Context
            .SetTable<Transport>()
            .GetById(entity.TransportId)
            .ExecuteAsync();

        if (!resultTransport.IsSuccess)
        {
            return resultTransport.Errors;
        }

        return await base.UpdateAsync(entity);
    }
}
