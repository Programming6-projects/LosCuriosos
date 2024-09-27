namespace DistributionCenter.Api.Tests.Controllers.Concretes;

using Application.Repositories.Interfaces;
using Domain.Entities.Concretes;
using Domain.Entities.Enums;
using Microsoft.AspNetCore.Mvc;

public class TripControllerTests
{
    private readonly TripController _controllerMock;
    private readonly Mock<IRepository<Trip>> _repositoryMock;
    private readonly Mock<IRepository<Transport>> _transportRepositoryMock;

    public TripControllerTests()
    {
        _transportRepositoryMock = new Mock<IRepository<Transport>>();
        _repositoryMock = new Mock<IRepository<Trip>>();
        _controllerMock = new TripController(_repositoryMock.Object, _transportRepositoryMock.Object);
    }

    [Fact]
    public void Constructor_InitializesController()
    {
        // Define Input and Output
        TripController controller = new(_repositoryMock.Object, _transportRepositoryMock.Object);

        // Verify actual result
        Assert.NotNull(controller);
    }

    [Fact]
    public async Task CompleteTrip_AllOrdersComplete_ShouldUpdateAvailability()
    {
        // Define Input and Output
        Guid clientId = new("51cc5b50-0bc5-4bf0-80e6-d7a0492e8f53");
        Guid tripId = Guid.NewGuid();
        Guid transportId = Guid.NewGuid();
        Transport transport = new()
        {
            Id = transportId,
            Name = "SomeTruck",
            Plate = "4233DFE",
            Capacity = 600000,
            CurrentCapacity = 600000,
            IsAvailable = true
        };
        Trip trip = new()
        {
            Id = tripId,
            Orders =
            [
                new Order
                {
                    Status = Status.Delivered,
                    ClientId = default,
                    RouteId = default,
                    DeliveryPointId = default,
                    DeliveryTime = null
                }
            ],
            TransportId = transportId,
            Status = Status.Pending
        };

        _ = _repositoryMock.Setup(repo => repo.GetByIdAsync(tripId))
            .ReturnsAsync(trip);
        _ = _repositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Trip>()))
            .ReturnsAsync(trip);
        _ = _transportRepositoryMock.Setup(repo => repo.GetByIdAsync(transportId))
            .ReturnsAsync(transport);
        _ = _transportRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Transport>()))
            .ReturnsAsync(transport);

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
        Guid transportId = Guid.NewGuid();
        Transport transport = new()
        {
            Id = transportId,
            Name = "SomeTruck",
            Plate = "4233DFE",
            Capacity = 600000,
            CurrentCapacity = 600000,
            IsAvailable = true
        };
        Trip trip = new()
        {
            Id = tripId,
            Orders =
            [
                new Order
                {
                    Status = Status.Pending,
                    ClientId = default,
                    RouteId = default,
                    DeliveryPointId = default,
                    DeliveryTime = null
                }
            ],
            TransportId = transportId,
            Status = Status.Pending
        };

        _ = _repositoryMock.Setup(repo => repo.GetByIdAsync(tripId))
            .ReturnsAsync(trip);
        _ = _repositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Trip>()))
            .ReturnsAsync(trip);
        _ = _transportRepositoryMock.Setup(repo => repo.GetByIdAsync(transportId))
            .ReturnsAsync(transport);
        _ = _transportRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Transport>()))
            .ReturnsAsync(transport);

        // Actions
        IActionResult result = await _controllerMock.CompleteTrip(tripId);

        // Verify actual result
        ObjectResult badRequestResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(409, badRequestResult.StatusCode);
    }
}
