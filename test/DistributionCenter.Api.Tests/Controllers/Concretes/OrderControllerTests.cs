namespace DistributionCenter.Api.Tests.Controllers.Concretes;

using DistributionCenter.Application.Repositories.Interfaces;
using DistributionCenter.Commons.Errors;
using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Services.Notification.Interfaces;
using Domain.Entities.Enums;
using Infraestructure.DTOs.Concretes.OrderProducts;
using Infraestructure.DTOs.Concretes.Orders;
using Microsoft.AspNetCore.Mvc;

public class OrderControllerTests
{
    private readonly Mock<IRepository<Order>> _orderRepositoryMock;
    private readonly Mock<IRepository<Client>> _clientRepositoryMock;
    private readonly Mock<IEmailService> _emailServiceMock;
    private readonly OrderController _controller;

    public OrderControllerTests()
    {
        _orderRepositoryMock = new Mock<IRepository<Order>>();
        _clientRepositoryMock = new Mock<IRepository<Client>>();
        _emailServiceMock = new Mock<IEmailService>();
        _controller = new OrderController(
            _orderRepositoryMock.Object,
            _clientRepositoryMock.Object,
            _emailServiceMock.Object
        );
    }

    [Fact]
    public async Task SendEmailByStatus_ReturnsOkResult_WhenOrderAndClientAreFound()
    {
        // Arrange
        Guid orderId = Guid.NewGuid();
        Order order =
            new()
            {
                Id = orderId,
                RouteId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                DeliveryPointId = Guid.NewGuid(),
                Status = Status.Pending,
                DeliveryTime = DateTime.Now,
            };
        Client client =
            new()
            {
                Name = "Name",
                LastName = "Last Name",
                Email = "test@example.com",
            };

        _ = _orderRepositoryMock.Setup(repo => repo.GetByIdAsync(orderId)).ReturnsAsync(order);
        _ = _clientRepositoryMock.Setup(repo => repo.GetByIdAsync(order.ClientId)).ReturnsAsync(client);

        // Act
        IActionResult result = await _controller.SendEmailByStatus(orderId);

        // Assert
        _ = Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SendEmailByStatus_ReturnsErrorResponse_WhenOrderNotFound()
    {
        // Arrange
        Guid orderId = Guid.NewGuid();
        _ = _orderRepositoryMock
            .Setup(repo => repo.GetByIdAsync(orderId))
            .ReturnsAsync(Error.NotFound(description: "Order not found"));

        // Act
        IActionResult result = await _controller.SendEmailByStatus(orderId);

        // Assert
        ObjectResult badRequestResult = Assert.IsType<ObjectResult>(result);
        _ = Assert.IsType<ProblemDetails>(badRequestResult.Value);
    }

    [Fact]
    public async Task SendEmailByStatus_ReturnsErrorResponse_WhenClientNotFound()
    {
        // Arrange
        Guid orderId = Guid.NewGuid();
        Order order =
            new()
            {
                Id = orderId,
                RouteId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                DeliveryPointId = Guid.NewGuid(),
                Status = Status.Pending,
                DeliveryTime = null,
            };

        _ = _orderRepositoryMock.Setup(repo => repo.GetByIdAsync(orderId)).ReturnsAsync(order);
        _ = _clientRepositoryMock
            .Setup(repo => repo.GetByIdAsync(order.ClientId))
            .ReturnsAsync(Error.NotFound(description: "Client not found"));

        // Act
        IActionResult result = await _controller.SendEmailByStatus(orderId);

        // Assert
        ObjectResult badRequestResult = Assert.IsType<ObjectResult>(result);
        _ = Assert.IsType<ProblemDetails>(badRequestResult.Value);
    }

    [Fact]
    public async Task Create_ReturnsOkResult_WhenOrderIsCreatedSuccessfully()
    {
        CreateOrderDto createOrderDto = new()
        {
            ClientId = Guid.NewGuid(),
            DeliveryPointId = Guid.NewGuid(),
            Products = new List<CreateOrderProductDto>()
        };
        Order order = new()
        {
            Id = Guid.NewGuid(),
            ClientId = Guid.NewGuid(),
            Status = Status.Pending,
            DeliveryPointId = Guid.NewGuid(),
            DeliveryTime = DateTime.Now
        };
        OkObjectResult okResult = new(order);

        _ = _orderRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Order>()))
            .ReturnsAsync(order);

        _ = _orderRepositoryMock.Setup(repo => repo.GetByIdAsync(order.Id))
            .ReturnsAsync(order);

        Client client = new()
        {
            Id = order.ClientId,
            Email = "test@example.com",
            Name = "Mayerli",
            LastName = "Santander"
        };
        _ = _clientRepositoryMock.Setup(repo => repo.GetByIdAsync(order.ClientId))
            .ReturnsAsync(client);

        IActionResult result = await _controller.Create(createOrderDto);

        // Assert
        OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(result);
        Order createdOrder = Assert.IsType<Order>(okObjectResult.Value);
        Assert.Equal(order.Id, createdOrder.Id);

        _orderRepositoryMock.Verify(repo => repo.GetByIdAsync(order.Id), Times.Once);
        _clientRepositoryMock.Verify(repo => repo.GetByIdAsync(order.ClientId), Times.Once);
    }

    [Fact]
    public async Task Create_ReturnsBadRequestResult_WhenOrderCreationFails()
    {
        CreateOrderDto createOrderDto = new()
        {
            ClientId = Guid.NewGuid(),
            DeliveryPointId = Guid.NewGuid(),
            Products = new List<CreateOrderProductDto>()
        };

        Order createdOrder = new()
        {
            Id = Guid.NewGuid(),
            ClientId = createOrderDto.ClientId,
            DeliveryPointId = createOrderDto.DeliveryPointId,
            Status = Status.Pending,
            DeliveryTime = DateTime.Now
        };

        _ = _orderRepositoryMock
            .Setup(repo => repo.CreateAsync(It.IsAny<Order>()))
            .ReturnsAsync(createdOrder);

        _ = _orderRepositoryMock
            .Setup(repo => repo.GetByIdAsync(createdOrder.Id))
            .ReturnsAsync(createdOrder);

        Client client = new()
        {
            Id = createOrderDto.ClientId,
            Email = "test@example.com",
            Name = "User",
            LastName = "UserLastName"
        };

        _ = _clientRepositoryMock
            .Setup(repo => repo.GetByIdAsync(createOrderDto.ClientId))
            .ReturnsAsync(client);

        IActionResult result = await _controller.Create(createOrderDto);

        // Assert
        OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
        Order orderResult = Assert.IsType<Order>(okResult.Value);
        Assert.Equal(createdOrder.Id, orderResult.Id);
        Assert.Equal(createdOrder.ClientId, orderResult.ClientId);
        Assert.Equal(createdOrder.DeliveryPointId, orderResult.DeliveryPointId);
    }
}
