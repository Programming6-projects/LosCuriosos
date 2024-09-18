namespace DistributionCenter.Api.Controllers.Concretes;

using Application.Repositories.Interfaces;
using Bases;
using Domain.Entities.Concretes;
using Infraestructure.DTOs.Concretes.Trip;
using Microsoft.AspNetCore.Mvc;

[Route("api/trips")]
public class RouteController(IRepository<Trip> repository)
    : BaseEntityController<Trip, CreateTripDto, UpdateTripDto> (repository)
{

}
