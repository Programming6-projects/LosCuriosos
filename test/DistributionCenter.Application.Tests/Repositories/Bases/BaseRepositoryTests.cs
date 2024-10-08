namespace DistributionCenter.Application.Tests.Repositories.Bases;

using DistributionCenter.Application.Contexts.Interfaces;
using DistributionCenter.Application.Repositories.Bases;
using DistributionCenter.Application.Tables.Components.QueryCommands.Interfaces;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;

public class BaseRepositoryTests
{
    private readonly Mock<IContext> _contextMock;
    private readonly Mock<BaseRepository<IEntity>> _repositoryMock;

    public BaseRepositoryTests()
    {
        _contextMock = new Mock<IContext>();
        _repositoryMock = new Mock<BaseRepository<IEntity>>(_contextMock.Object) { CallBase = true };
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnEntity()
    {
        // Define Input and Output
        Mock<IQuery<IEntity>> mockQuery = new();
        Result<IEntity> expectedResult = new(new Mock<IEntity>().Object);

        _ = mockQuery.Setup(static q => q.ExecuteAsync()).ReturnsAsync(expectedResult);
        _ = _contextMock.Setup(static c => c.SetTable<IEntity>().GetById(It.IsAny<Guid>())).Returns(mockQuery.Object);

        // Execute actual operation
        Result<IEntity> result = await _repositoryMock.Object.GetByIdAsync(Guid.NewGuid());

        // Verify actual result
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task GetAll_ShouldReturnListOfEntities()
    {
        // Define Input and Output
        Mock<IQuery<IEnumerable<IEntity>>> mockQuery = new();

        List<IEntity> mockEntities = [new Mock<IEntity>().Object];

        Result<IEnumerable<IEntity>> expectedResult = mockEntities;

        _ = mockQuery.Setup(static q => q.ExecuteAsync()).ReturnsAsync(expectedResult);
        _ = _contextMock.Setup(static c => c.SetTable<IEntity>().GetAll()).Returns(mockQuery.Object);

        // Execute actual operation
        Result<IEnumerable<IEntity>> result = await _repositoryMock.Object.GetAllAsync();

        // Verify actual result
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnEntity()
    {
        // Define Input and Output
        Mock<ICommand> mockCommand = new();

        _ = mockCommand.Setup(static c => c.ExecuteAsync()).ReturnsAsync(Result.Ok());
        _ = _contextMock
            .Setup(static c => c.SetTable<IEntity>().Create(It.IsAny<IEntity>()))
            .Returns(mockCommand.Object);

        // Execute actual operation
        Result result = await _repositoryMock.Object.CreateAsync(new Mock<IEntity>().Object);

        // Verify actual result
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task CreateAllAsync_ShouldReturnRowsAffected()
    {
        // Define input entities
        List<IEntity> entities = new()
        {
            new Mock<IEntity>().Object,
            new Mock<IEntity>().Object
        };

        // Mock the behavior for each entity creation
        Mock<ICommand> mockCommand = new();
        _ = mockCommand.Setup(c => c.ExecuteAsync()).ReturnsAsync(Result.Ok());

        _ = _contextMock
            .Setup(static c => c.SetTable<IEntity>().Create(It.IsAny<IEntity>()))
            .Returns(mockCommand.Object);

        // Execute the actual operation
        Result<int> result = await _repositoryMock.Object.CreateAllAsync(entities);

        // Verify that rows affected are equal to the number of entities
        Assert.True(result.IsSuccess);
        Assert.Equal(entities.Count, result.Value);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnEntity()
    {
        // Define Input and Output
        Mock<ICommand> mockCommand = new();

        _ = mockCommand.Setup(static c => c.ExecuteAsync()).ReturnsAsync(Result.Ok());
        _ = _contextMock
            .Setup(static c => c.SetTable<IEntity>().Update(It.IsAny<IEntity>()))
            .Returns(mockCommand.Object);

        // Exectue actual operation
        Result result = await _repositoryMock.Object.UpdateAsync(new Mock<IEntity>().Object);

        // Verify actual result
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateAllAsync_ShouldReturnRowsAffected()
    {
        // Define input entities
        List<IEntity> entities = new()
        {
            new Mock<IEntity>().Object,
            new Mock<IEntity>().Object
        };

        // Mock the behavior for each entity update
        Mock<ICommand> mockCommand = new();
        _ = mockCommand.Setup(c => c.ExecuteAsync()).ReturnsAsync(Result.Ok());

        _ = _contextMock
            .Setup(static c => c.SetTable<IEntity>().Update(It.IsAny<IEntity>()))
            .Returns(mockCommand.Object);

        // Execute the actual operation
        Result<int> result = await _repositoryMock.Object.UpdateAllAsync(entities);

        // Verify that rows affected are equal to the number of entities
        Assert.True(result.IsSuccess);
        Assert.Equal(entities.Count, result.Value);
    }
}
