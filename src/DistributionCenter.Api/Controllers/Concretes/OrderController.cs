namespace DistributionCenter.Api.Controllers.Concretes;

using System.ComponentModel.DataAnnotations;
using DistributionCenter.Api.Controllers.Bases;
using DistributionCenter.Api.Controllers.Extensions;
using DistributionCenter.Application.Repositories.Interfaces;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Services.Notification.Interfaces;
using Infraestructure.DTOs.Concretes.Orders;
using Microsoft.AspNetCore.Mvc;
using Services.Notification.Dtos;

[Route("api/orders")]
public class OrderController(
    IRepository<Order> orderRepository,
    IRepository<Client> clientRepository,
    IEmailService emailService
) : BaseEntityController<Order, CreateOrderDto, UpdateOrderDto>(orderRepository)
{
    [HttpGet("{id}/status")]
    public async Task<IActionResult> SendEmailByStatus([FromRoute] [Required] Guid id)
    {
        Result<Order> orderResult = await Repository.GetByIdAsync(id);
        if (!orderResult.IsSuccess) return this.ErrorsResponse(orderResult.Errors);

        Order order = orderResult.Value;
        Result<Client> clientResult = await clientRepository.GetByIdAsync(order.ClientId);
        if (!clientResult.IsSuccess) return this.ErrorsResponse(clientResult.Errors);

        Client client = clientResult.Value;
        OrderDto orderDto = new() { OrderId = order.Id, OrderStatus = order.Status, TimeToDeliver = order.DeliveryTime };

        IMessage message = NotificationFactory.CreateMessage(orderDto);
        _ = Task.Run(() => emailService.SendEmailAsync(client.Email, message));

        return Ok(new { OrderId = order.Id, client.Email, Status = order.Status.GetDescription(), });
    }
}
