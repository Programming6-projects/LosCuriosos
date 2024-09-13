namespace DistributionCenter.Api.Controllers.Concretes;

using DistributionCenter.Api.Controllers.Bases;
using DistributionCenter.Application.Repositories.Interfaces;
using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Infraestructure.DTOs.Concretes.Clients;
using Microsoft.AspNetCore.Mvc;

[Route("api/clients")]
public class ClientController(IRepository<Client> repository)
    : BaseEntityController<Client, CreateClientDto, UpdateClientDto>(repository) { }
