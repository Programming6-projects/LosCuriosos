namespace DistributionCenter.Api.Controllers.Concretes;

using Application.Repositories.Interfaces;
using Bases;
using Domain.Entities.Concretes;
using Infraestructure.DTOs.Concretes.DeliveryPoint;
using Microsoft.AspNetCore.Mvc;

[Route("api/delivery-point")]
public class DeliveryPointController(
    IRepository<DeliveryPoint> repository
) : BaseEntityController<DeliveryPoint, CreateDeliveryPointDto, UpdateDeliveryPointDto>(repository) { }
