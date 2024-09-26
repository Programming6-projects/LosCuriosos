namespace DistributionCenter.Api.Tests.Controllers.Concretes;

using DistributionCenter.Api.Controllers.Concretes;
using DistributionCenter.Application.Repositories.Interfaces;
using DistributionCenter.Commons.Errors;
using DistributionCenter.Domain.Entities.Concretes;
using Microsoft.AspNetCore.Mvc;

public class StrikeControllerTests
{
    private readonly Mock<IRepository<Strike>> _strikeRepositoryMock;
    private readonly Mock<IRepository<Transport>> _transportRepositoryMock;
    private readonly StrikeController _controller;

    public StrikeControllerTests()
    {
        _strikeRepositoryMock = new Mock<IRepository<Strike>>();
        _transportRepositoryMock = new Mock<IRepository<Transport>>();
        _controller = new StrikeController(_strikeRepositoryMock.Object, _transportRepositoryMock.Object);
    }

    [Fact]
    public async Task PaidStrikesFromTransport_ReturnsOkResult_WhenTransportAndStrikesAreFound()
    {
        // Arrange
        Guid transportId = Guid.NewGuid();
        Transport transport =
            new()
            {
                Id = transportId,
                Name = "test",
                Plate = "ASDNGDK",
                Capacity = 10,
                CurrentCapacity = 10,
                IsAvailable = true,
            };
        List<Strike> strikes =
        [
            new()
            {
                Description = "Test",
                TransportId = transportId,
                IsActive = true,
            },
            new()
            {
                Description = "Test",
                TransportId = transportId,
                IsActive = true,
            },
        ];

        _ = _transportRepositoryMock.Setup(repo => repo.GetByIdAsync(transportId)).ReturnsAsync(transport);
        _ = _strikeRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(strikes);
        _ = _strikeRepositoryMock
            .Setup(repo => repo.UpdateAsync(It.IsAny<Strike>()))
            .ReturnsAsync((Strike strike) => strike);

        // Act
        IActionResult result = await _controller.PaidStrikesFromTransport(transportId);

        // Assert
        _ = Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task PaidStrikesFromTransport_ReturnsErrorResponse_WhenTransportNotFound()
    {
        // Arrange
        Guid transportId = Guid.NewGuid();
        _ = _transportRepositoryMock
            .Setup(repo => repo.GetByIdAsync(transportId))
            .ReturnsAsync(Error.NotFound("TRANSPORT_NOT_FOUND"));

        // Act
        IActionResult result = await _controller.PaidStrikesFromTransport(transportId);

        // Assert
        ObjectResult badRequestResult = Assert.IsType<ObjectResult>(result);
        _ = Assert.IsType<ProblemDetails>(badRequestResult.Value);
    }

    [Fact]
    public async Task PaidStrikesFromTransport_ReturnsErrorResponse_WhenStrikesNotFound()
    {
        // Arrange
        Guid transportId = Guid.NewGuid();
        Transport transport =
            new()
            {
                Id = transportId,
                Name = "test",
                Plate = "ASDNGDK",
                Capacity = 10,
                CurrentCapacity = 10,
                IsAvailable = true,
            };

        _ = _transportRepositoryMock.Setup(repo => repo.GetByIdAsync(transportId)).ReturnsAsync(transport);
        _ = _strikeRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(Error.NotFound("STRIKES_NOT_FOUND"));

        // Act
        IActionResult result = await _controller.PaidStrikesFromTransport(transportId);

        // Assert
        ObjectResult badRequestResult = Assert.IsType<ObjectResult>(result);
        _ = Assert.IsType<ProblemDetails>(badRequestResult.Value);
    }

    [Fact]
    public async Task PaidStrikesFromTransport_ReturnsErrorResponse_WhenUpdateFails()
    {
        // Arrange
        Guid transportId = Guid.NewGuid();
        Transport transport =
            new()
            {
                Id = transportId,
                Name = "test",
                Plate = "ASDNGDK",
                Capacity = 10,
                CurrentCapacity = 10,
                IsAvailable = true,
            };
        List<Strike> strikes =
        [
            new()
            {
                Description = "Test",
                TransportId = transportId,
                IsActive = true,
            },
            new()
            {
                Description = "Test",
                TransportId = transportId,
                IsActive = true,
            },
        ];

        _ = _transportRepositoryMock.Setup(repo => repo.GetByIdAsync(transportId)).ReturnsAsync(transport);
        _ = _strikeRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(strikes);
        _ = _strikeRepositoryMock
            .Setup(repo => repo.UpdateAsync(It.IsAny<Strike>()))
            .ReturnsAsync(Error.NotFound("UPDATE_FAILED"));

        // Act
        IActionResult result = await _controller.PaidStrikesFromTransport(transportId);

        // Assert
        ObjectResult badRequestResult = Assert.IsType<ObjectResult>(result);
        _ = Assert.IsType<ProblemDetails>(badRequestResult.Value);
    }
}
