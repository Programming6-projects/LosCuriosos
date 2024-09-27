namespace DistributionCenter.Api.Tests.Controllers.Concretes;

using Application.Repositories.Interfaces;
using Commons.Errors;
using Commons.Results;
using Domain.Entities.Concretes;
using Domain.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Services.Distribution.Enums;
using Services.Distribution.Interfaces;
using Services.Notification.Interfaces;
using Services.Routes.Interfaces;

public class BusinessActionsControllerTests
{
    private readonly Mock<IRepository<Order>> _orderRepositoryMock = new();
    private readonly Mock<IRepository<Transport>> _transportRepositoryMock = new();
    private readonly Mock<IRepository<Client>> _clientRepositoryMock = new();
    private readonly Mock<IRepository<Trip>> _tripRepositoryMock = new();
    private readonly Mock<IRepository<DeliveryPoint>> _deliveryPointRepositoryMock = new();
    private readonly Mock<IDistributionStrategy> _distributionStrategy = new();
    private readonly Mock<IEmailService> _emailService = new();
    private readonly Mock<IRouteService> _routeService = new();

    [Fact]
    public void Constructor_InitializesController()
    {
        // Define Input and Output
        BusinessActionsController controller = new(
            _orderRepositoryMock.Object,
            _transportRepositoryMock.Object,
            _clientRepositoryMock.Object,
            _tripRepositoryMock.Object,
            _distributionStrategy.Object,
            _deliveryPointRepositoryMock.Object,
            _emailService.Object,
            _routeService.Object
            );

        // Verify actual result
        Assert.NotNull(controller);
    }

    [Fact]
    public async Task StartDistribution_WithoutOrdersAvailable()
    {
        // Define Input and Output
        Location location = Location.InCity;
        BusinessActionsController controller = new(
            _orderRepositoryMock.Object,
            _transportRepositoryMock.Object,
            _clientRepositoryMock.Object,
            _tripRepositoryMock.Object,
            _distributionStrategy.Object,
            _deliveryPointRepositoryMock.Object,
            _emailService.Object,
            _routeService.Object);

        Transport[] transports =
        [
            new() {
                Id = Guid.NewGuid(),
                IsAvailable = true,
                IsActive = true,
                Name = "VAN523",
                Plate = "5324FSD",
                Capacity = 0,
                CurrentCapacity = 0
            }
        ];

        Trip[] trips =
        [
            new()
            {
                Id = Guid.NewGuid(),
                Status = Status.Pending,
                TransportId = null
            }
        ];

        Order[] cancelledOrders = Array.Empty<Order>();

        _ = _orderRepositoryMock
            .Setup(r => r.SelectWhereAsync(It.IsAny<Func<Order, bool>>()))
            .ReturnsAsync(Error.NotFound());
        _ = _transportRepositoryMock
            .Setup(r => r.SelectWhereAsync(It.IsAny<Func<Transport, bool>>()))
            .ReturnsAsync(new Result<IEnumerable<Transport>>(transports));
        _ = _tripRepositoryMock
            .Setup(r => r.SelectWhereAsync(It.IsAny<Func<Trip, bool>>()))
            .ReturnsAsync(new Result<IEnumerable<Trip>>(trips));
        _ = _distributionStrategy.Setup(ds => ds.DistributeOrders(
                It.IsAny<ICollection<Order>>(),
                It.IsAny<ICollection<Transport>>(),
                It.IsAny<Location>()))
            .Returns((trips, transports, cancelledOrders));

        IActionResult result = await controller.StartDistribution(location);

        ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(404, objectResult.StatusCode);
    }

    [Fact]
    public async Task StartDistribution_WithoutTransportsAvailable()
    {
        // Define Input and Output
        Location location = Location.InCity;
        BusinessActionsController controller = new(
            _orderRepositoryMock.Object,
            _transportRepositoryMock.Object,
            _clientRepositoryMock.Object,
            _tripRepositoryMock.Object,
            _distributionStrategy.Object,
            _deliveryPointRepositoryMock.Object,
            _emailService.Object,
            _routeService.Object);

        Order[] orders =
        [
            new()
            {
                Id = Guid.NewGuid(),
                Status = Status.Pending,
                IsActive = true,
                RouteId = default,
                ClientId = default,
                DeliveryPointId = default,
                DeliveryTime = null
            }
        ];

        Transport[] transports = {
            new()
            {
                Id = Guid.NewGuid(),
                IsAvailable = false,
                IsActive = true,
                Name = "VAN523",
                Plate = "5324FSD",
                Capacity = 0,
                CurrentCapacity = 0
            }
        };

        Trip[] trips = {
            new()
            {
                Id = Guid.NewGuid(),
                Status = Status.Pending,
                TransportId = null
            }
        };

        Order[] cancelledOrders = Array.Empty<Order>();

        _ = _orderRepositoryMock
            .Setup(r => r.SelectWhereAsync(It.IsAny<Func<Order, bool>>()))
            .ReturnsAsync(new Result<IEnumerable<Order>>(orders));
        _ = _transportRepositoryMock
            .Setup(r => r.SelectWhereAsync(It.IsAny<Func<Transport, bool>>()))
            .ReturnsAsync(Error.NotFound());
        _ = _tripRepositoryMock
            .Setup(r => r.SelectWhereAsync(It.IsAny<Func<Trip, bool>>()))
            .ReturnsAsync(new Result<IEnumerable<Trip>>(trips));
        _ = _distributionStrategy.Setup(ds => ds.DistributeOrders(
                It.IsAny<ICollection<Order>>(),
                It.IsAny<ICollection<Transport>>(),
                It.IsAny<Location>()))
            .Returns((trips, transports, cancelledOrders));

        IActionResult result = await controller.StartDistribution(location);

        ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(404, objectResult.StatusCode);
    }

    [Fact]
    public void UpdateTables()
    {
        // Define Input and Output

        Guid tripId = Guid.NewGuid();

        IEnumerable<Order> orders =
        [
            new()
            {
                RouteId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                DeliveryPointId = Guid.NewGuid(),
                Status = Status.Pending,
                DeliveryTime = null
            }
        ];
        IEnumerable<Trip> trips =
        [
            new()
            {
                Id = tripId,
                Status = Status.Pending,
                TransportId = null
            }
        ];
        IEnumerable<Transport> transports =
        [
            new()
            {
                Name = "VAN54",
                Plate = "3252DGD",
                Capacity = 500000000,
                CurrentCapacity = 500000000,
                IsAvailable = true
            }
        ];

        _ = _orderRepositoryMock
            .Setup(r => r.UpdateAllAsync(It.IsAny<IEnumerable<Order>>()))
            .ReturnsAsync(orders.Count());
        _ = _transportRepositoryMock
            .Setup(r => r.UpdateAllAsync(It.IsAny<IEnumerable<Transport>>()))
            .ReturnsAsync(transports.Count());
        _ = _tripRepositoryMock
            .Setup(r => r.UpdateAllAsync(It.IsAny<IEnumerable<Trip>>()))
            .ReturnsAsync(trips.Count());

        //controller.StartDistribution();

        // Verify actual result
    }

    [Fact]
    public async Task UpdateTables_UpdatesTransportStatusToUnavailable()
    {
        // Arrange
        Transport[] transports = new[]
        {
            new Transport
            {
                Id = Guid.NewGuid(),
                IsAvailable = true,
                Name = "TRUCK42",
                Plate = "523ASF",
                Capacity = 0,
                CurrentCapacity = 0
            }
        };

        (IEnumerable<Trip> Trips, Transport[] UpdatedTransports, IEnumerable<Order> CancelledOrders) distributionResult
            = (
            Trips: Enumerable.Empty<Trip>(),
            UpdatedTransports: transports,
            CancelledOrders: Enumerable.Empty<Order>()
        );

        BusinessActionsController controller = new(
            _orderRepositoryMock.Object,
            _transportRepositoryMock.Object,
            _clientRepositoryMock.Object,
            _tripRepositoryMock.Object,
            _distributionStrategy.Object,
            _deliveryPointRepositoryMock.Object,
            _emailService.Object,
            _routeService.Object);

        // Act
        _ = _orderRepositoryMock.Setup(r => r.UpdateAllAsync(It.IsAny<IEnumerable<Order>>()))
            .ReturnsAsync(new Result<int>(0));
        _ = _transportRepositoryMock.Setup(r => r.UpdateAllAsync(It.IsAny<IEnumerable<Transport>>()))
            .ReturnsAsync(new Result<int>(1));
        _ = _tripRepositoryMock.Setup(r => r.CreateAllAsync(It.IsAny<IEnumerable<Trip>>()))
            .ReturnsAsync(new Result<int>(0));

        int rowsAffected = await controller.UpdateTables(Enumerable.Empty<Order>(), distributionResult);

        // Assert
        Assert.Equal(1, rowsAffected);
        _transportRepositoryMock.Verify(r => r.UpdateAllAsync(It.IsAny<IEnumerable<Transport>>()), Times.Once);
    }

    [Fact]
    public async Task UpdateTables_UpdatesOrdersStatusToSending()
    {
        // Arrange
        Order[] orders = new[]
        {
            new Order
            {
                RouteId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                DeliveryPointId = Guid.NewGuid(),
                Status = Status.Pending,
                DeliveryTime = null
            }
        };

        (IEnumerable<Trip> Trips, Transport[] UpdatedTransports, IEnumerable<Order> CancelledOrders) distributionResult
            = (
                Trips: [],
                UpdatedTransports: [],
                CancelledOrders: []
            );

        BusinessActionsController controller = new(
            _orderRepositoryMock.Object,
            _transportRepositoryMock.Object,
            _clientRepositoryMock.Object,
            _tripRepositoryMock.Object,
            _distributionStrategy.Object,
            _deliveryPointRepositoryMock.Object,
            _emailService.Object,
            _routeService.Object);

        // Act
        _ = _orderRepositoryMock.Setup(r => r.UpdateAllAsync(It.IsAny<IEnumerable<Order>>()))
            .ReturnsAsync(new Result<int>(1));
        _ = _transportRepositoryMock.Setup(r => r.UpdateAllAsync(It.IsAny<IEnumerable<Transport>>()))
            .ReturnsAsync(new Result<int>(0));
        _ = _tripRepositoryMock.Setup(r => r.CreateAllAsync(It.IsAny<IEnumerable<Trip>>()))
            .ReturnsAsync(new Result<int>(0));

        int rowsAffected = await controller.UpdateTables(orders, distributionResult);

        // Assert
        Assert.Equal(2, rowsAffected);
        _transportRepositoryMock.Verify(r => r.UpdateAllAsync(It.IsAny<IEnumerable<Transport>>()), Times.Once);
    }
}
