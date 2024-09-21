namespace DistributionCenter.Api.Controllers.Concretes;

using Application.Repositories.Interfaces;
using Bases;
using Domain.Entities.Concretes;
using Microsoft.AspNetCore.Mvc;

[Route("api/transports")]
public class TransportController(IRepository<Transport> repository)
: BaseEntityController<Transport, CreateTransportDto, UpdateTransportDto>(repository) { }
