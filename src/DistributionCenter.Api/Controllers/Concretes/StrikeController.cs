namespace DistributionCenter.Api.Controllers.Concretes;

using System.ComponentModel.DataAnnotations;
using DistributionCenter.Api.Controllers.Bases;
using DistributionCenter.Api.Controllers.Extensions;
using DistributionCenter.Application.Repositories.Interfaces;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Concretes;
using Microsoft.AspNetCore.Mvc;

[Route("api/strikes")]
public class StrikeController(IRepository<Strike> repository, IRepository<Transport> transportRepository)
    : BaseEntityController<Strike, CreateStrikeDto, UpdateStrikeDto>(repository)
{
    [HttpPatch("{transportId}")]
    public async Task<IActionResult> PaidStrikesFromTransport([FromRoute] [Required] Guid transportId)
    {
        Result<Transport> resultTransport = await transportRepository.GetByIdAsync(transportId);

        if (!resultTransport.IsSuccess)
        {
            return this.ErrorsResponse(resultTransport.Errors);
        }

        Transport transport = resultTransport.Value;

        Result<IEnumerable<Strike>> resultStrikes = await Repository.GetAllAsync();

        if (!resultStrikes.IsSuccess)
        {
            return this.ErrorsResponse(resultStrikes.Errors);
        }

        IEnumerable<Strike> transportStrikes = resultStrikes.Value.Where(strike => strike.TransportId == transport.Id);

        foreach (Strike strike in transportStrikes)
        {
            strike.IsActive = false;

            Result<Strike> resultUpdate = await Repository.UpdateAsync(strike);

            if (!resultUpdate.IsSuccess)
            {
                return this.ErrorsResponse(resultUpdate.Errors);
            }
        }

        return Ok();
    }
}
