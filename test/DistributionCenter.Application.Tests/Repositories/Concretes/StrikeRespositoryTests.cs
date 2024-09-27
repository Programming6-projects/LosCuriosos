namespace DistributionCenter.Application.Tests.Repositories.Concretes;

using DistributionCenter.Application.Contexts.Interfaces;
using DistributionCenter.Application.Tables.Components.QueryCommands.Interfaces;
using DistributionCenter.Application.Tables.Core.Interfaces;
using DistributionCenter.Commons.Errors;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Concretes;

public class StrikeRepositoryTests
{
    private readonly StrikeRepository _strikeRepository;
    private readonly Mock<IContext> _contextMock;
    private readonly Mock<ITable<Strike>> _strikeTableMock;
    private readonly Mock<ITable<Transport>> _transportTableMock;
    private readonly List<Strike> _existingStrikes;
    private readonly Transport _transport;
    private readonly Strike _strike;

    public StrikeRepositoryTests()
    {
        _contextMock = new Mock<IContext>();
        _strikeTableMock = new Mock<ITable<Strike>>();
        _transportTableMock = new Mock<ITable<Transport>>();

        _strike = new() { Description = "Strike Description", TransportId = Guid.NewGuid() };

        _transport = new()
        {
            Id = _strike.TransportId,
            Name = "Truck",
            Capacity = 10,
            CurrentCapacity = 10,
            Plate = "12523AAG",
            IsAvailable = true,
        };

        _ = _contextMock.Setup(static context => context.SetTable<Strike>()).Returns(_strikeTableMock.Object);
        _ = _contextMock.Setup(static context => context.SetTable<Transport>()).Returns(_transportTableMock.Object);

        _existingStrikes = [_strike];

        _strikeRepository = new StrikeRepository(_contextMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ReturnsStrike_WhenTransportExists()
    {
        // Define Input and Output
        Mock<IQuery<Transport>> query = new();
        _ = query.Setup(q => q.ExecuteAsync()).ReturnsAsync(_transport);
        _ = _transportTableMock.Setup(table => table.GetById(_strike.TransportId)).Returns(query.Object);

        Mock<IQuery<IEnumerable<Strike>>> queryStrikes = new();
        _ = queryStrikes.Setup(q => q.ExecuteAsync()).ReturnsAsync(_existingStrikes);
        _ = _strikeTableMock.Setup(table => table.GetAll()).Returns(queryStrikes.Object);

        Mock<ICommand> createStrikeCommand = new();
        _ = createStrikeCommand.Setup(command => command.ExecuteAsync()).ReturnsAsync(Result.Ok());
        _ = _strikeTableMock.Setup(table => table.Create(_strike)).Returns(createStrikeCommand.Object);

        Mock<ICommand> updateTransportCommand = new();
        _ = updateTransportCommand.Setup(command => command.ExecuteAsync()).ReturnsAsync(Result.Ok());
        _ = _transportTableMock.Setup(table => table.Update(_transport)).Returns(updateTransportCommand.Object);

        // Execute actual operation
        Result<Strike> result = await _strikeRepository.CreateAsync(_strike);

        // Verify actual result
        Assert.True(result.IsSuccess);
        Assert.Equal(_strike, result.Value);
    }

    [Fact]
    public async Task CreateAsync_ReturnsError_WhenTransportDoesNotExist()
    {
        // Define Input and Output
        Mock<IQuery<Transport>> query = new();
        _ = query.Setup(static q => q.ExecuteAsync()).ReturnsAsync(Error.NotFound("TRANSPORT_NOT_FOUND"));
        _ = _transportTableMock.Setup(table => table.GetById(_strike.TransportId)).Returns(query.Object);

        // Execute actual operation
        Result<Strike> result = await _strikeRepository.CreateAsync(_strike);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, error => error.Code == "TRANSPORT_NOT_FOUND");
    }

    [Fact]
    public async Task UpdateAsync_ReturnsStrike_WhenTransportExists()
    {
        // Define Input and Output
        Mock<IQuery<Transport>> query = new();
        _ = query.Setup(q => q.ExecuteAsync()).ReturnsAsync(_transport);
        _ = _transportTableMock.Setup(table => table.GetById(_strike.TransportId)).Returns(query.Object);

        Mock<IQuery<IEnumerable<Strike>>> queryStrikes = new();
        _ = queryStrikes.Setup(q => q.ExecuteAsync()).ReturnsAsync(_existingStrikes);
        _ = _strikeTableMock.Setup(table => table.GetAll()).Returns(queryStrikes.Object);

        Mock<ICommand> updateStrikeCommand = new();
        _ = updateStrikeCommand.Setup(command => command.ExecuteAsync()).ReturnsAsync(Result.Ok());
        _ = _strikeTableMock.Setup(table => table.Update(_strike)).Returns(updateStrikeCommand.Object);

        Mock<ICommand> updateTransportCommand = new();
        _ = updateTransportCommand.Setup(command => command.ExecuteAsync()).ReturnsAsync(Result.Ok());
        _ = _transportTableMock.Setup(table => table.Update(_transport)).Returns(updateTransportCommand.Object);

        // Execute actual operation
        Result<Strike> result = await _strikeRepository.UpdateAsync(_strike);

        // Verify actual result
        Assert.True(result.IsSuccess);
        Assert.Equal(_strike, result.Value);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsError_WhenTransportDoesNotExist()
    {
        // Define Input and Output
        Mock<IQuery<Transport>> query = new();
        _ = query.Setup(static q => q.ExecuteAsync()).ReturnsAsync(Error.NotFound("TRANSPORT_NOT_FOUND"));
        _ = _transportTableMock.Setup(table => table.GetById(_strike.TransportId)).Returns(query.Object);

        // Execute actual operation
        Result<Strike> result = await _strikeRepository.UpdateAsync(_strike);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, error => error.Code == "TRANSPORT_NOT_FOUND");
    }

    [Fact]
    public async Task CreateAsync_SetsTransportToUnavailable_WhenThreeActiveStrikesExist()
    {
        // Arrange
        List<Strike> existingStrikes = new()
        {
            new()
            {
                Id = Guid.NewGuid(),
                TransportId = _transport.Id,
                IsActive = true,
                Description = " aaaa"
            },
            new()
            {
                Id = Guid.NewGuid(),
                TransportId = _transport.Id,
                IsActive = true,
                Description = " aaaa"
            },
            new()
            {
                Id = _strike.Id,
                TransportId = _transport.Id,
                IsActive = true,
                Description = " aaaa"
            }
        };

        SetupMocks(existingStrikes, _transport);

        // Act
        Result<Strike> result = await _strikeRepository.CreateAsync(_strike);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(_strike, result.Value);
        Assert.False(_transport.IsAvailable);
        _transportTableMock.Verify(t => t.Update(_transport), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_KeepsTransportAvailable_WhenLessThanThreeActiveStrikesExist()
    {
        // Arrange
        List<Strike> existingStrikes = new()
        {
            new()
            {
                Id = Guid.NewGuid(),
                TransportId = _transport.Id,
                IsActive = true,
                Description = " aaaa"
            },
            new()
            {
                Id = Guid.NewGuid(),
                TransportId = _transport.Id,
                IsActive = true,
                Description = " aaaa"
            }
        };

        SetupMocks(existingStrikes, _transport);

        // Act
        Result<Strike> result = await _strikeRepository.CreateAsync(_strike);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(_strike, result.Value);
        Assert.True(_transport.IsAvailable);
        _transportTableMock.Verify(t => t.Update(_transport), Times.Never);
    }

    [Fact]
    public async Task UpdateAsync_SetsTransportToUnavailable_WhenUpdatingThirdActiveStrike()
    {
        // Arrange
        List<Strike> existingStrikes = new()
        {
            new()
            {
                Id = Guid.NewGuid(),
                TransportId = _transport.Id,
                IsActive = true,
                Description = " aaaa"
            },
            new()
            {
                Id = Guid.NewGuid(),
                TransportId = _transport.Id,
                IsActive = true,
                Description = " aaaa"
            },
            new()
            {
                Id = _strike.Id,
                TransportId = _transport.Id,
                IsActive = false,
                Description = " aaaa"
            }
        };

        SetupMocks(existingStrikes, _transport);

        _strike.IsActive = true;

        // Act
        Result<Strike> result = await _strikeRepository.UpdateAsync(_strike);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(_strike, result.Value);
    }

    [Fact]
    public async Task UpdateAsync_SetsTransportToAvailable_WhenDeactivatingThirdStrike()
    {
        // Arrange
        _transport.IsAvailable = false;
        List<Strike> existingStrikes = new()
        {
            new()
            {
                Id = Guid.NewGuid(),
                TransportId = _transport.Id,
                IsActive = true,
                Description = " aaaa"
            },
            new()
            {
                Id = Guid.NewGuid(),
                TransportId = _transport.Id,
                IsActive = true,
                Description = " aaaa"
            },
            new()
            {
                Id = _strike.Id,
                TransportId = _transport.Id,
                IsActive = true,
                Description = " aaaa"
            }
        };

        SetupMocks(existingStrikes, _transport);

        _strike.IsActive = false;

        // Act
        Result<Strike> result = await _strikeRepository.UpdateAsync(_strike);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(_strike, result.Value);
        _transportTableMock.Verify(t => t.Update(_transport), Times.Once);
    }

    private void SetupMocks(List<Strike> strikes, Transport transport)
    {
        _ = _transportTableMock.Setup(t => t.GetById(transport.Id))
            .Returns(Mock.Of<IQuery<Transport>>(q => q.ExecuteAsync() == Task.FromResult(new Result<Transport>(transport))));

        _ = _strikeTableMock.Setup(t => t.GetAll())
            .Returns(Mock.Of<IQuery<IEnumerable<Strike>>>(q => q.ExecuteAsync() == Task.FromResult(new Result<IEnumerable<Strike>>(strikes))));

        _ = _strikeTableMock.Setup(t => t.Create(It.IsAny<Strike>()))
            .Returns(Mock.Of<ICommand>(c => c.ExecuteAsync() == Task.FromResult(Result.Ok())));

        _ = _strikeTableMock.Setup(t => t.Update(It.IsAny<Strike>()))
            .Returns(Mock.Of<ICommand>(c => c.ExecuteAsync() == Task.FromResult(Result.Ok())));

        _ = _transportTableMock.Setup(t => t.Update(It.IsAny<Transport>()))
            .Returns(Mock.Of<ICommand>(c => c.ExecuteAsync() == Task.FromResult(Result.Ok())));
    }
}
