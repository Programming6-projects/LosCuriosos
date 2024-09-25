namespace DistributionCenter.Application.Repositories.Concretes;

using Bases;
using Contexts.Interfaces;
using DistributionCenter.Commons.Results;
using Domain.Entities.Concretes;

public class StrikeRepository(IContext context) : BaseRepository<Strike>(context)
{
    private async Task<Result> VerifyThreeStrikesTransport(Strike entity)
    {
        Result<IEnumerable<Strike>> resultStrikes = await Context.SetTable<Strike>().GetAll().ExecuteAsync();

        if (!resultStrikes.IsSuccess)
        {
            return resultStrikes.Errors;
        }

        IEnumerable<Strike> transportStrikes = resultStrikes.Value.Where(s => s.TransportId == entity.TransportId);

        if (transportStrikes.Count(static s => s.IsActive) < 3)
        {
            return Result.Ok();
        }

        Result<Transport> resultTransport = await Context
            .SetTable<Transport>()
            .GetById(entity.TransportId)
            .ExecuteAsync();

        if (!resultTransport.IsSuccess)
        {
            return resultTransport.Errors;
        }

        Transport transport = resultTransport.Value;

        transport.IsAvailable = false;

        return await Context.SetTable<Transport>().Update(transport).ExecuteAsync();
    }

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

        Result<Strike> resultStrike = await base.CreateAsync(entity);

        Result resultVerify = await VerifyThreeStrikesTransport(entity);

        if (!resultVerify.IsSuccess)
        {
            return resultVerify.Errors;
        }

        return resultStrike;
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

        Result<Strike> resultStrike = await base.UpdateAsync(entity);

        Result resultVerify = await VerifyThreeStrikesTransport(entity);

        if (!resultVerify.IsSuccess)
        {
            return resultVerify.Errors;
        }

        return resultStrike;
    }
}
