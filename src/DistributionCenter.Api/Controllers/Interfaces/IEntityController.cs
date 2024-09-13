namespace DistributionCenter.Api.Controllers.Interfaces;

using DistributionCenter.Domain.Entities.Interfaces;
using DistributionCenter.Infraestructure.DTOs.Interfaces;
using Microsoft.AspNetCore.Mvc;

public interface IEntityController<TEntity, TCreateDto, TUpdateDto>
    where TEntity : IEntity
    where TCreateDto : ICreateDto<TEntity>
    where TUpdateDto : IUpdateDto<TEntity>
{
    Task<IActionResult> Create(TCreateDto request);

    Task<IActionResult> GetById(Guid id);

    Task<IActionResult> Update(Guid id, TUpdateDto request);

    Task<IActionResult> Disable(Guid id);
}
