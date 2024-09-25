namespace DistributionCenter.Api.Controllers.Concretes;

using System.ComponentModel.DataAnnotations;
using Commons.Errors;
using Commons.Errors.Interfaces;
using DistributionCenter.Api.Controllers.Bases;
using DistributionCenter.Api.Controllers.Extensions;
using DistributionCenter.Application.Repositories.Interfaces;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Services.Notification.Interfaces;
using Infraestructure.DTOs.Concretes.Orders;
using Microsoft.AspNetCore.Mvc;

[Route("api/orders")]
public class OrderController(
    IRepository<Order> orderRepository,
    IRepository<OrderProduct> orderProductRepository,
    IRepository<Product> productRepository,
    IRepository<DeliveryPoint> deliveryPointRepository,
    IRepository<Client> clientRepository,
    IEmailService emailService
) : BaseEntityController<Order, CreateOrderDto, UpdateOrderDto>(orderRepository)
{

    [HttpGet("{id}/status")]
    public async Task<IActionResult> SendEmailByStatus([FromRoute] [Required] Guid id)
    {
        Result<Order> orderResult = await Repository.GetByIdAsync(id);

        if (!orderResult.IsSuccess)
        {
            return this.ErrorsResponse(orderResult.Errors);
        }

        Order order = orderResult.Value;

        Result<Client> clientResult = await clientRepository.GetByIdAsync(order.ClientId);

        if (!clientResult.IsSuccess)
        {
            return this.ErrorsResponse(clientResult.Errors);
        }

        Client client = clientResult.Value;

        IMessage message = NotificationFactory.CreateMessage(order.Status, order.Id);

        _ = Task.Run(() => emailService.SendEmailAsync(client.Email, message));

        return Ok(
            new
            {
                OrderId = order.Id,
                client.Email,
                Status = order.Status.GetDescription(),
            }
        );
    }

    [HttpPost("createOrderWithNotification")]
    public async Task<IActionResult> CreateOrder(CreateOrderDto request)
    {
        Result validateResult = request.Validate();

        if (!validateResult.IsSuccess)
        {
            return this.ErrorsResponse(validateResult.Errors);
        }

        foreach (OrderProductRequestDto orderProductRequestDto in request.OrderProducts)
        {
            Result<Product> productResult = await productRepository.GetByIdAsync(orderProductRequestDto.ProductId);
            if (!productResult.IsSuccess)
            {
                IError error = Error.NotFound();
                List<IError> errors = new();
                errors.Add(error);
                return this.ErrorsResponse(errors);
            }
        }

        Result<DeliveryPoint> coordinates = await deliveryPointRepository.GetByIdAsync(request.DeliveryPointId);
        if (!coordinates.IsSuccess)
        {
            IError error = Error.NotFound();
            List<IError> errors = new();
            errors.Add(error);
            return this.ErrorsResponse(errors);
        }

        Order entity = request.ToEntity();

        Result<Order> result = await Repository.CreateAsync(entity);
        List<OrderProduct> OrderProducts = new();
        foreach (OrderProductRequestDto orderProductRequestDto in request.OrderProducts)
        {
            Result<Product> productResult = await productRepository.GetByIdAsync(orderProductRequestDto.ProductId);
            Product product = productResult.Value;
            CreateOrderProductDto orderProductDto = new()
            {
                ProductId = orderProductRequestDto.ProductId,
                OrderId = entity.Id,
                Quantity = orderProductRequestDto.Quantity,
                Product = product
            };
            OrderProduct orderProduct = orderProductDto.ToEntity();
            OrderProducts.Add(orderProduct);
            _ = await orderProductRepository.CreateAsync(orderProduct);
        }

        entity.Products = OrderProducts;

        _ = SendEmailByStatus(entity.Id);
        return result.Match(entity => Ok(entity), this.ErrorsResponse);
    }
}
