namespace DistributionCenter.Api.Controllers.Concretes;

using DistributionCenter.Api.Controllers.Bases;
using DistributionCenter.Application.Repositories.Interfaces;
using DistributionCenter.Domain.Entities.Concretes;
using Infraestructure.DTOs.Concretes.Orders;
using Microsoft.AspNetCore.Mvc;

[Route("api/orders")]
public class OrderController(IRepository<Order> repository)
    : BaseEntityController<Order, CreateOrderDto, UpdateOrderDto>(repository) { }