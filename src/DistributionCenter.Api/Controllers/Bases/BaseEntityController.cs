namespace DistributionCenter.Api.Controllers.Bases;

using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;
using DistributionCenter.Infraestructure.Mediators.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public abstract class BaseEntityController<TEntity, TCreateDto, TUpdateDto>(ISender mediator) : BaseApiController
    where TEntity : IEntity
{
    protected ISender Mediator { get; } = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateClient(TCreateDto request)
    {
        CreateCommand<TEntity, TCreateDto> command = new() { Dto = request };

        Result<TEntity> result = await Mediator.Send(command);

        return result.Match(entity => Ok(entity), Problem);
    }
    //
    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetClientById([FromRoute] [Required] Guid id)
    // {
    //     GetClientByIdQuery query = new() { Id = id };
    //
    //     Result<Client> result = await _mediator.Send(query);
    //
    //     return result.Match(Ok, Problem);
    // }

    // [HttpPut("{id}")]
    // public async Task<IActionResult> UpdateClient([FromRoute] [Required] Guid id, UpdateClientDto request)
    // {
    //     UpdateClientCommand command = new() { Id = id, ClientDto = request };
    //
    //     Result<Client> result = await _mediator.Send(command);
    //
    //     return result.Match(Ok, Problem);
    // }
    //
    // [HttpPatch("{id}/disable")]
    // public async Task<IActionResult> DisableClient([FromRoute] [Required] Guid id)
    // {
    //     SwitchClientCommand command = new() { Id = id, IsActive = false };
    //
    //     Result<Client> result = await _mediator.Send(command);
    //
    //     return result.Match(Ok, Problem);
    // }
    //
    // [HttpPatch("{id}/enable")]
    // public async Task<IActionResult> EnableClient([FromRoute] [Required] Guid id)
    // {
    //     SwitchClientCommand command = new() { Id = id, IsActive = true };
    //
    //     Result<Client> result = await _mediator.Send(command);
    //
    //     return result.Match(Ok, Problem);
    // }
}
