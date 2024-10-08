namespace DistributionCenter.Application.Tests.Repositories.Concretes;

using DistributionCenter.Application.Contexts.Interfaces;
using DistributionCenter.Application.Tables.Components.QueryCommands.Interfaces;
using DistributionCenter.Application.Tables.Core.Interfaces;
using DistributionCenter.Commons.Errors;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Concretes;

public class TransportRepositoryTests
{
    private readonly TransportRepository _transportRepository;
    private readonly Mock<IContext> _contextMock;
    private readonly Mock<ITable<Transport>> _transportTableMock;
    private readonly Mock<ITable<Strike>> _strikeTableMock;
    private readonly Transport _transport;
    private readonly List<Strike> _strikes;

    public TransportRepositoryTests()
    {
        _contextMock = new Mock<IContext>();
        _transportTableMock = new Mock<ITable<Transport>>();
        _strikeTableMock = new Mock<ITable<Strike>>();

        _transport = new()
        {
            Id = Guid.NewGuid(),
            Name = "Truck",
            Capacity = 10,
            CurrentCapacity = 10,
            Plate = "12523AAG",
            IsAvailable = true,
        };

        _strikes =
        [
            new() { TransportId = _transport.Id, Description = "Strike 1" },
            new() { TransportId = _transport.Id, Description = "Strike 2" },
        ];

        _ = _contextMock.Setup(static context => context.SetTable<Transport>()).Returns(_transportTableMock.Object);
        _ = _contextMock.Setup(static context => context.SetTable<Strike>()).Returns(_strikeTableMock.Object);

        _transportRepository = new TransportRepository(_contextMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsTransportWithStrikes_WhenTransportExists()
    {
        // Define Input and Output
        Mock<IQuery<Transport>> queryTransport = new();
        _ = queryTransport.Setup(q => q.ExecuteAsync()).ReturnsAsync(_transport);
        _ = _transportTableMock.Setup(table => table.GetById(_transport.Id)).Returns(queryTransport.Object);

        Mock<IQuery<IEnumerable<Strike>>> queryStrikes = new();
        _ = queryStrikes.Setup(q => q.ExecuteAsync()).ReturnsAsync(_strikes);
        _ = _strikeTableMock.Setup(table => table.GetAll()).Returns(queryStrikes.Object);

        // Execute actual operation
        Result<Transport> result = await _transportRepository.GetByIdAsync(_transport.Id);

        // Verify actual result
        Assert.True(result.IsSuccess);
        Assert.Equal(_transport, result.Value);
        Assert.Equal(_strikes, result.Value.Strikes);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsError_WhenTransportDoesNotExist()
    {
        // Define Input and Output
        Mock<IQuery<Transport>> queryTransport = new();
        _ = queryTransport.Setup(q => q.ExecuteAsync()).ReturnsAsync(Error.NotFound("TRANSPORT_NOT_FOUND"));
        _ = _transportTableMock.Setup(table => table.GetById(_transport.Id)).Returns(queryTransport.Object);

        // Execute actual operation
        Result<Transport> result = await _transportRepository.GetByIdAsync(_transport.Id);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, error => error.Code == "TRANSPORT_NOT_FOUND");
    }

    [Fact]
    public async Task UpdateAsync_ReturnsTransportWithStrikes_WhenUpdateIsSuccessful()
    {
        // Define Input and Output
        Mock<ICommand> updateTransportCommand = new();
        _ = updateTransportCommand.Setup(command => command.ExecuteAsync()).ReturnsAsync(Result.Ok());
        _ = _transportTableMock.Setup(table => table.Update(_transport)).Returns(updateTransportCommand.Object);

        Mock<IQuery<IEnumerable<Strike>>> queryStrikes = new();
        _ = queryStrikes.Setup(q => q.ExecuteAsync()).ReturnsAsync(_strikes);
        _ = _strikeTableMock.Setup(table => table.GetAll()).Returns(queryStrikes.Object);

        // Execute actual operation
        Result<Transport> result = await _transportRepository.UpdateAsync(_transport);

        // Verify actual result
        Assert.True(result.IsSuccess);
        Assert.Equal(_transport, result.Value);
        Assert.Equal(_strikes, result.Value.Strikes);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsError_WhenUpdateFails()
    {
        // Define Input and Output
        Mock<ICommand> updateTransportCommand = new();
        _ = updateTransportCommand
            .Setup(command => command.ExecuteAsync())
            .ReturnsAsync(Error.NotFound("UPDATE_FAILED"));
        _ = _transportTableMock.Setup(table => table.Update(_transport)).Returns(updateTransportCommand.Object);

        // Execute actual operation
        Result<Transport> result = await _transportRepository.UpdateAsync(_transport);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, error => error.Code == "UPDATE_FAILED");
    }


    [Fact]
    public async Task GetByIdAsync_ReturnsTransportWithNoStrikes_WhenNoStrikesExist()
    {
        // Arrange
        Mock<IQuery<Transport>> queryTransport = new();
        _ = queryTransport.Setup(q => q.ExecuteAsync()).ReturnsAsync(_transport);
        _ = _transportTableMock.Setup(table => table.GetById(_transport.Id)).Returns(queryTransport.Object);

        Mock<IQuery<IEnumerable<Strike>>> queryStrikes = new();
        _ = queryStrikes.Setup(q => q.ExecuteAsync()).ReturnsAsync(new List<Strike>());
        _ = _strikeTableMock.Setup(table => table.GetAll()).Returns(queryStrikes.Object);

        // Act
        Result<Transport> result = await _transportRepository.GetByIdAsync(_transport.Id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(_transport, result.Value);
        Assert.Empty(result.Value.Strikes);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsError_WhenStrikeQueryFails()
    {
        // Arrange
        Mock<IQuery<Transport>> queryTransport = new();
        _ = queryTransport.Setup(q => q.ExecuteAsync()).ReturnsAsync(_transport);
        _ = _transportTableMock.Setup(table => table.GetById(_transport.Id)).Returns(queryTransport.Object);

        Mock<IQuery<IEnumerable<Strike>>> queryStrikes = new();
        _ = queryStrikes.Setup(q => q.ExecuteAsync()).ReturnsAsync(Error.Unexpected("STRIKE_QUERY_FAILED"));
        _ = _strikeTableMock.Setup(table => table.GetAll()).Returns(queryStrikes.Object);

        // Act
        Result<Transport> result = await _transportRepository.GetByIdAsync(_transport.Id);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, error => error.Code == "STRIKE_QUERY_FAILED");
    }

    [Fact]
    public async Task UpdateAsync_ReturnsTransportWithUpdatedStrikes_WhenUpdateIsSuccessful()
    {
        // Arrange
        List<Strike> updatedStrikes =
        [
            new() { TransportId = _transport.Id, Description = "Updated Strike 1" },
            new() { TransportId = _transport.Id, Description = "Updated Strike 2" },
            new() { TransportId = _transport.Id, Description = "New Strike 3" },
        ];

        Mock<ICommand> updateTransportCommand = new();
        _ = updateTransportCommand.Setup(command => command.ExecuteAsync()).ReturnsAsync(Result.Ok());
        _ = _transportTableMock.Setup(table => table.Update(_transport)).Returns(updateTransportCommand.Object);

        Mock<IQuery<IEnumerable<Strike>>> queryStrikes = new();
        _ = queryStrikes.Setup(q => q.ExecuteAsync()).ReturnsAsync(updatedStrikes);
        _ = _strikeTableMock.Setup(table => table.GetAll()).Returns(queryStrikes.Object);

        // Act
        Result<Transport> result = await _transportRepository.UpdateAsync(_transport);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(_transport, result.Value);
        Assert.Equal(updatedStrikes, result.Value.Strikes);
        Assert.Equal(3, result.Value.Strikes.Count());
    }

    [Fact]
    public async Task UpdateAsync_ReturnsError_WhenStrikeQueryFailsAfterUpdate()
    {
        // Arrange
        Mock<ICommand> updateTransportCommand = new();
        _ = updateTransportCommand.Setup(command => command.ExecuteAsync()).ReturnsAsync(Result.Ok());
        _ = _transportTableMock.Setup(table => table.Update(_transport)).Returns(updateTransportCommand.Object);

        Mock<IQuery<IEnumerable<Strike>>> queryStrikes = new();
        _ = queryStrikes.Setup(q => q.ExecuteAsync()).ReturnsAsync(Error.Unexpected("STRIKE_QUERY_FAILED"));
        _ = _strikeTableMock.Setup(table => table.GetAll()).Returns(queryStrikes.Object);

        // Act
        Result<Transport> result = await _transportRepository.UpdateAsync(_transport);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, error => error.Code == "STRIKE_QUERY_FAILED");
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsTransportWithCorrectStrikes_WhenMultipleTransportsExist()
    {
        // Arrange
        Transport anotherTransport = new()
        {
            Id = Guid.NewGuid(),
            Name = "Another Truck",
            Capacity = 15,
            CurrentCapacity = 15,
            Plate = "98765ZYX",
            IsAvailable = true,
        };

        List<Strike> allStrikes =
        [
            new() { TransportId = _transport.Id, Description = "Strike 1 for Transport 1" },
            new() { TransportId = _transport.Id, Description = "Strike 2 for Transport 1" },
            new() { TransportId = anotherTransport.Id, Description = "Strike 1 for Transport 2" },
            new() { TransportId = anotherTransport.Id, Description = "Strike 2 for Transport 2" },
        ];

        Mock<IQuery<Transport>> queryTransport = new();
        _ = queryTransport.Setup(q => q.ExecuteAsync()).ReturnsAsync(_transport);
        _ = _transportTableMock.Setup(table => table.GetById(_transport.Id)).Returns(queryTransport.Object);

        Mock<IQuery<IEnumerable<Strike>>> queryStrikes = new();
        _ = queryStrikes.Setup(q => q.ExecuteAsync()).ReturnsAsync(allStrikes);
        _ = _strikeTableMock.Setup(table => table.GetAll()).Returns(queryStrikes.Object);

        // Act
        Result<Transport> result = await _transportRepository.GetByIdAsync(_transport.Id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(_transport, result.Value);
        Assert.Equal(2, result.Value.Strikes.Count());
        Assert.All(result.Value.Strikes, strike => Assert.Equal(_transport.Id, strike.TransportId));
    }
}
