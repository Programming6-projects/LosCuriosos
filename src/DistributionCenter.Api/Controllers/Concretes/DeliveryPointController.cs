namespace DistributionCenter.Api.Controllers.Concretes;

using DistributionCenter.Api.Controllers.Bases;
using DistributionCenter.Application.Repositories.Interfaces;
using DistributionCenter.Domain.Entities.Concretes;
using Infraestructure.DTOs.Concretes.DeliveryPoint;
using Microsoft.AspNetCore.Mvc;

[Route("api/delivery_point")]
public class DeliveryPointController(IRepository<DeliveryPoint> repository)
    : BaseEntityController<DeliveryPoint, CreateDeliveryPointDto, UpdateDeliveryPointDto>(repository) { }
