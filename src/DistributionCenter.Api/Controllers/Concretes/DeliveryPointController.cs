
namespace DistributionCenter.Api.Controllers.Concretes;

using Application.Repositories.Interfaces;
using Bases;
using Infraestructure.DTOs.Concretes.DeliveryPoint;
using Domain.Entities.Concretes;
using Microsoft.AspNetCore.Mvc;

[Route("api/deliveryPoint")]
public class DeliveryPointController(
    IRepository<DeliveryPoint> repository
) : BaseEntityController<DeliveryPoint, CreateDeliveryPointDto, UpdateDeliveryPointDto>(repository)
{ }
