namespace DistributionCenter.Api.Tests.Controllers.Concretes;

using Application.Repositories.Interfaces;
using Commons.Errors;
using Commons.Results;
using Domain.Entities.Concretes;
using Domain.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Services.Distribution.Enums;
using Services.Distribution.Interfaces;

public class BusinessActionsControllerTests
{
    private readonly Mock<IRepository<Order>> _orderRepositoryMock;
    private readonly Mock<IRepository<Transport>> _transportRepositoryMock;
    private readonly Mock<IRepository<Trip>> _tripRepositoryMock;
    private readonly Mock<IDistributionStrategy> _distributionStrategy;

    public BusinessActionsControllerTests()
    {
        _orderRepositoryMock = new Mock<IRepository<Order>>();
        _transportRepositoryMock = new Mock<IRepository<Transport>>();
        _tripRepositoryMock = new Mock<IRepository<Trip>>();
        _distributionStrategy = new Mock<IDistributionStrategy>();
    }

    [Fact]
    public void Constructor_InitializesController()
    {
        // Define Input and Output
        BusinessActionsController controller = new(
            _orderRepositoryMock.Object,
            _transportRepositoryMock.Object,
            _tripRepositoryMock.Object,
            _distributionStrategy.Object);

        // Verify actual result
        Assert.NotNull(controller);
    }

    [Fact]
    public async Task StartDistribution_WithAvailableTransportOnCity()
    {
        // Define Input and Output
        Location location = Location.InCity;
        BusinessActionsController controller = new(
            _orderRepositoryMock.Object,
            _transportRepositoryMock.Object,
            _tripRepositoryMock.Object,
            _distributionStrategy.Object);

        Guid tripId = Guid.NewGuid();

        Order[] orders = new[]
        {
            new Order
            {
                Id = Guid.NewGuid(),
                Status = Status.Pending,
                IsActive = true,
                RouteId = default,
                ClientId = default,
                DeliveryPointId = default
            }
        };

        Transport[] transports = new[]
        {
            new Transport
            {
                Id = Guid.NewGuid(),
                IsAvailable = true,
                IsActive = true,
                Name = "VAN523",
                Plate = "5324FSD",
                Capacity = 10000,
                CurrentCapacity = 10000
            }
        };

        Trip[] trips = new[]
        {
            new Trip
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
        _ = _orderRepositoryMock
            .Setup(r => r.UpdateAllAsync(It.IsAny<IEnumerable<Order>>()))
            .ReturnsAsync(new Result<int>(1));
        _ = _transportRepositoryMock
            .Setup(r => r.SelectWhereAsync(It.IsAny<Func<Transport, bool>>()))
            .ReturnsAsync(new Result<IEnumerable<Transport>>(transports));
        _ = _transportRepositoryMock
            .Setup(r => r.UpdateAllAsync(It.IsAny<IEnumerable<Transport>>()))
            .ReturnsAsync(new Result<int>(1));
        _ = _tripRepositoryMock
            .Setup(r => r.SelectWhereAsync(It.IsAny<Func<Trip, bool>>()))
            .ReturnsAsync(new Result<IEnumerable<Trip>>(trips));
        _ = _tripRepositoryMock
            .Setup(r => r.CreateAllAsync(It.IsAny<IEnumerable<Trip>>()))
            .ReturnsAsync(new Result<int>(1));
        _ = _distributionStrategy.Setup(ds => ds.DistributeOrders(
                It.IsAny<ICollection<Order>>(),
                It.IsAny<ICollection<Transport>>(),
                It.IsAny<Location>()))
            .Returns((trips, transports, cancelledOrders));

        IActionResult result = await controller.StartDistribution(location);

        OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
        DistributionResult resultData = Assert.IsType<DistributionResult>(okResult.Value!);

        // Verify actual result
        Assert.Equal(1, resultData.OrdersAssigned);
        Assert.Equal(1, resultData.TripsCount);
        Assert.Equal(1, resultData.UpdatedTransportsCount);
        Assert.Equal(0, resultData.CancelledOrdersCount);

        // Verify calls to internal repositories
        _transportRepositoryMock.Verify(r => r.SelectWhereAsync(It.IsAny<Func<Transport, bool>>()), Times.Once);
        _tripRepositoryMock.Verify(r => r.CreateAllAsync(It.IsAny<IEnumerable<Trip>>()), Times.Once);
        _orderRepositoryMock.Verify(r => r.UpdateAllAsync(It.IsAny<IEnumerable<Order>>()), Times.Exactly(2));
    }

    [Fact]
    public async Task StartDistribution_WithoutOrdersAvailable()
    {
        // Define Input and Output
        Location location = Location.InCity;
        BusinessActionsController controller = new(
            _orderRepositoryMock.Object,
            _transportRepositoryMock.Object,
            _tripRepositoryMock.Object,
            _distributionStrategy.Object);

        Guid tripId = Guid.NewGuid();

        Order[] orders = Array.Empty<Order>();

        Transport[] transports = new[]
        {
            new Transport
            {
                Id = Guid.NewGuid(),
                IsAvailable = true,
                IsActive = true,
                Name = "VAN523",
                Plate = "5324FSD",
                Capacity = 0,
                CurrentCapacity = 0
            }
        };

        Trip[] trips = new[]
        {
            new Trip
            {
                Id = Guid.NewGuid(),
                Status = Status.Pending,
                TransportId = null
            }
        };

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
            _tripRepositoryMock.Object,
            _distributionStrategy.Object);

        Guid tripId = Guid.NewGuid();

        Order[] orders = new[]
        {
            new Order
            {
                Id = Guid.NewGuid(),
                Status = Status.Pending,
                IsActive = true,
                RouteId = default,
                ClientId = default,
                DeliveryPointId = default
            }
        };

        Transport[] transports = new[]
        {
            new Transport
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

        Trip[] trips = new[]
        {
            new Trip
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
        BusinessActionsController controller = new(
            _orderRepositoryMock.Object,
            _transportRepositoryMock.Object,
            _tripRepositoryMock.Object,
            _distributionStrategy.Object);

        Guid tripId = Guid.NewGuid();

        IEnumerable<Order> orders = new []
        {
            new Order
            {
                RouteId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                DeliveryPointId = Guid.NewGuid(),
                Status = Status.Pending
            }
        };
        IEnumerable<Trip> trips = new []
        {
            new Trip
            {
                Id = tripId,
                Status = Status.Pending,
                TransportId = null
            }
        };
        IEnumerable<Transport> transports = new []
        {
            new Transport
            {
                Name = "VAN54",
                Plate = "3252DGD",
                Capacity = 500000000,
                CurrentCapacity = 500000000,
                IsAvailable = true
            }
        };
        IEnumerable<Order> cancelledOrders = [];

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
            _tripRepositoryMock.Object,
            _distributionStrategy.Object);

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
                Status = Status.Pending
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
            _tripRepositoryMock.Object,
            _distributionStrategy.Object);

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
