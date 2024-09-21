namespace DistributionCenter.Api.Tests.Controllers.Concretes;

using Application.Repositories.Interfaces;
using Domain.Entities.Concretes;
using Domain.Entities.Enums;
using Microsoft.AspNetCore.Mvc;

public class TripControllerTests
{
    private readonly TripController _controllerMock;
    private readonly Mock<IRepository<Trip>> _repositoryMock;

    public TripControllerTests()
    {
        _repositoryMock = new Mock<IRepository<Trip>>();
        _controllerMock = new TripController(_repositoryMock.Object);
    }

    [Fact]
    public void Constructor_InitializesController()
    {
        // Define Input and Output
        TripController controller = new(_repositoryMock.Object);

        // Verify actual result
        Assert.NotNull(controller);
    }

    [Fact]
    public async Task CompleteTrip_AllOrdersComplete_ShouldUpdateAvailability()
    {
        // Define Input and Output
        Guid clientId = new("51cc5b50-0bc5-4bf0-80e6-d7a0492e8f53");
        Guid tripId = Guid.NewGuid();
        Trip trip = new()
        {
            Id = tripId,
            Orders = new[]
            {
                new Order
                {
                    ClientId = clientId,
                    Status = Status.Shipped,
                    RouteId = default,
                    DeliveryPointId = default
                }
            },
            TransportId = Guid.NewGuid(),
            Status = Status.Pending
        };
        _ = _repositoryMock.Setup(repo => repo.GetByIdAsync(tripId))
            .ReturnsAsync(trip);
        _ = _repositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Trip>()))
            .ReturnsAsync(trip);

        // Actions
        IActionResult result = await _controllerMock.CompleteTrip(tripId);

        // Verify actual result
        OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult);
    }

    [Fact]
    public async Task CompleteTrip_NotAllOrdersComplete_ShouldReturnErrorsResponse()
    {
        // Define Input and Output
        Guid clientId = new("51cc5b50-0bc5-4bf0-80e6-d7a0492e8f53");
        Guid tripId = Guid.NewGuid();
        Trip trip = new()
        {
            Id = tripId,
            Orders = new[]
            {
                new Order
                {
                    Status = Status.Pending,
                    ClientId = default,
                    RouteId = default,
                    DeliveryPointId = default
                }
            },
            TransportId = Guid.NewGuid(),
            Status = Status.Pending
        };
        _ = _repositoryMock.Setup(repo => repo.GetByIdAsync(tripId))
            .ReturnsAsync(trip);

        // Actions
        IActionResult result = await _controllerMock.CompleteTrip(tripId);

        // Verify actual result
        ObjectResult badRequestResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(404, badRequestResult.StatusCode);
    }
}
