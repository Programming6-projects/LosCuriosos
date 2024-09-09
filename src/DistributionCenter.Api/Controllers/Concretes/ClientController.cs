namespace DistributionCenter.Api.Controllers.Concretes;

using DistributionCenter.Api.Controllers.Bases;
using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Infraestructure.DTOs.Clients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/clients")]
public class ClientController(ISender mediator)
    : BaseEntityController<Client, CreateClientDto, UpdateClientDto>(mediator)
{
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
