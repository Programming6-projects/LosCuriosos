namespace DistributionCenter.Application.Repositories.Concretes;

using Bases;
using Contexts.Interfaces;
using DistributionCenter.Commons.Results;
using Domain.Entities.Concretes;

public class TransportRepository(IContext context) : BaseRepository<Transport>(context)
{
    private async Task<Result<Transport>> GetTransportWithStrikes(Transport transport)
    {
        Result<IEnumerable<Strike>> resultStrikes = await Context.SetTable<Strike>().GetAll().ExecuteAsync();

        if (!resultStrikes.IsSuccess)
        {
            return resultStrikes.Errors;
        }

        transport.Strikes = resultStrikes.Value.Where(strike => strike.TransportId == transport.Id);

        return transport;
    }

    public override async Task<Result<Transport>> GetByIdAsync(Guid id)
    {
        Result<Transport> resultTransport = await base.GetByIdAsync(id);

        if (!resultTransport.IsSuccess)
        {
            return resultTransport.Errors;
        }

        return await GetTransportWithStrikes(resultTransport.Value);
    }

    public override async Task<Result<Transport>> UpdateAsync(Transport entity)
    {
        Result<Transport> resultTransport = await base.UpdateAsync(entity);

        if (!resultTransport.IsSuccess)
        {
            return resultTransport.Errors;
        }

        return await GetTransportWithStrikes(resultTransport.Value);
    }
}
