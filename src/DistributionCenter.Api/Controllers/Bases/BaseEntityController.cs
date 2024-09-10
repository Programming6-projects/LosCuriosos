namespace DistributionCenter.Api.Controllers.Bases;

using System.ComponentModel.DataAnnotations;
using DistributionCenter.Api.Controllers.Extensions;
using DistributionCenter.Api.Controllers.Interfaces;
using DistributionCenter.Application.Repositories.Interfaces;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;
using DistributionCenter.Infraestructure.DTOs.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public abstract class BaseEntityController<TEntity, TCreateDto, TUpdateDto>(IRepository<TEntity> repository)
    : ControllerBase,
        IEntityController<TEntity, TCreateDto, TUpdateDto>
    where TEntity : IEntity
    where TCreateDto : ICreateDto<TEntity>
    where TUpdateDto : IUpdateDto<TEntity>
{
    protected IRepository<TEntity> Repository { get; } = repository;

    [HttpPost]
    public async Task<IActionResult> Create(TCreateDto request)
    {
        TEntity entity = request.ToEntity();

        Result<TEntity> result = await Repository.CreateAsync(entity);

        return result.Match(entity => Ok(entity), this.ErrorsResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] [Required] Guid id)
    {
        Result<TEntity> result = await Repository.GetByIdAsync(id);

        return result.Match(entity => Ok(entity), this.ErrorsResponse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] [Required] Guid id, TUpdateDto request)
    {
        Result<TEntity> searchEntity = await Repository.GetByIdAsync(id);

        if (!searchEntity.IsSuccess)
        {
            return this.ErrorsResponse(searchEntity.Errors);
        }

        TEntity entity = request.FromEntity(searchEntity.Value);

        Result<TEntity> result = await Repository.UpdateAsync(entity);

        return result.Match(entity => Ok(entity), this.ErrorsResponse);
    }

    [HttpPatch("{id}/disable")]
    public async Task<IActionResult> Disable([FromRoute] [Required] Guid id)
    {
        Result<TEntity> searchEntity = await Repository.GetByIdAsync(id);

        if (!searchEntity.IsSuccess)
        {
            return this.ErrorsResponse(searchEntity.Errors);
        }

        TEntity entity = searchEntity.Value;

        entity.IsActive = false;

        Result<TEntity> result = await Repository.UpdateAsync(entity);

        return result.Match(entity => Ok(entity), this.ErrorsResponse);
    }
}
