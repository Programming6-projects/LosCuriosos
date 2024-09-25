namespace DistributionCenter.Api.Controllers.Concretes;

using DistributionCenter.Api.Controllers.Bases;
using DistributionCenter.Application.Repositories.Interfaces;
using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Infraestructure.DTOs.Concretes.Strikes;
using Microsoft.AspNetCore.Mvc;

[Route("api/strikes")]
public class StrikeController(IRepository<Strike> repository)
    : BaseEntityController<Strike, CreateStrikeDto, UpdateStrikeDto>(repository) { }
