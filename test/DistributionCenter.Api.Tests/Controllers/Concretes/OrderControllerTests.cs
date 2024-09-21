namespace DistributionCenter.Api.Tests.Controllers.Concretes;

using DistributionCenter.Api.Controllers.Concretes;
using DistributionCenter.Application.Repositories.Interfaces;
using DistributionCenter.Commons.Errors;
using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Domain.Entities.Enums;
using DistributionCenter.Services.Notification.Interfaces;
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
        Guid expectedClientId = Guid.NewGuid();
        Guid expectedDeliveredPointId = Guid.NewGuid();
        Guid expectedRouteId = Guid.NewGuid();
        Guid expectedProductId = Guid.NewGuid();
        Product product = new()
        {
            Name = "Pepsi",
            Description = "The best drink",
            Weight = 100,
        };

        ClientOrderProduct clientOrderProduct =
            new ()
            {
                ProductId = expectedProductId,
                OrderId = expectedClientId,
                Quantity = 0,
                Product = product
            };

        IReadOnlyList<ClientOrderProduct> expectedClientOrderProduct = new List<ClientOrderProduct> { clientOrderProduct };
        Order order =
            new()
            {
                Status = Status.Pending,
                RouteId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                DeliveryPointId = Guid.NewGuid(),
                Id = orderId,
                ClientOrderProducts = expectedClientOrderProduct
            };
        Client client =
            new()
            {
                Id = order.ClientId,
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
        Guid expectedClientId = Guid.NewGuid();
        Guid expectedDeliveredPointId = Guid.NewGuid();
        Guid expectedRouteId = Guid.NewGuid();
        Guid expectedProductId = Guid.NewGuid();
        Product product = new()
        {
            Name = "Pepsi",
            Description = "The best drink",
            Weight = 100,
        };

        ClientOrderProduct clientOrderProduct =
            new ()
            {
                ProductId = expectedProductId,
                OrderId = expectedClientId,
                Quantity = 0,
                Product = product
            };

        IReadOnlyList<ClientOrderProduct> expectedClientOrderProduct = new List<ClientOrderProduct> { clientOrderProduct };
        Order order =
            new()
            {
                Status = Status.Pending,
                RouteId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                DeliveryPointId = Guid.NewGuid(),
                Id = orderId,
                ClientOrderProducts = expectedClientOrderProduct
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
}
