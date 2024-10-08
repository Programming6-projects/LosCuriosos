namespace DistributionCenter.Application.Tests.Tables.Core.Bases;

using Application.Tables.Connections.File.Interfaces;
using DistributionCenter.Application.Tables.Components.Information.Interfaces;
using DistributionCenter.Application.Tables.Components.QueryCommands.Interfaces;
using DistributionCenter.Application.Tables.Core.Bases;
using Domain.Entities.Interfaces;
using Moq;
using Xunit;

public class BaseFileTableTests
{
    private readonly Mock<IFileConnectionFactory<IEntity>> _fileConnectionFactoryMock;
    private readonly Mock<BaseFileTable<IEntity>> _tableMock;
    private readonly Mock<ITableInformation> _infoMock;

    public BaseFileTableTests()
    {
        _fileConnectionFactoryMock = new Mock<IFileConnectionFactory<IEntity>>();
        _tableMock = new Mock<BaseFileTable<IEntity>>(_fileConnectionFactoryMock.Object) { CallBase = true };
        _infoMock = new Mock<ITableInformation>();
    }

    [Fact]
    public void GetById_ShouldReturnQuery()
    {
        // Define Input and Output
        _ = _infoMock.Setup(static i => i.TableName).Returns("table");
        _ = _tableMock.Setup(static t => t.GetInformation()).Returns(_infoMock.Object);

        // Execute actual operation
        IQuery<IEntity> query = _tableMock.Object.GetById(Guid.NewGuid());

        // Verify actual result
        Assert.NotNull(query);
        _ = Assert.IsType<GetByIdJsonQuery<IEntity>>(query);
    }

    [Fact]
    public void GetAll_ShouldReturnQuery()
    {
        // Define Input and Output
        _ = _infoMock.Setup(static i => i.TableName).Returns("table");
        _ = _tableMock.Setup(static t => t.GetInformation()).Returns(_infoMock.Object);

        // Execute actual operation
        IQuery<IEnumerable<IEntity>> query = _tableMock.Object.GetAll();

        // Verify actual result
        Assert.NotNull(query);
        _ = Assert.IsType<GetAllJsonQuery<IEntity>>(query);
    }

    [Fact]
    public void Create_ShouldReturnCommand()
    {
        // Define Input and Output
        _ = _infoMock.Setup(static i => i.TableName).Returns("table");
        _ = _tableMock.Setup(static t => t.GetInformation()).Returns(_infoMock.Object);
        Mock<IEntity> entityMock = new();

        // Execute actual operation
        ICommand command = _tableMock.Object.Create(entityMock.Object);

        // Verify actual result
        Assert.NotNull(command);
        _ = Assert.IsType<CreateJsonCommand<IEntity>>(command);
    }

    [Fact]
    public void Update_ShouldReturnCommand()
    {
        // Define Input and Output
        _ = _infoMock.Setup(static i => i.TableName).Returns("table");
        _ = _tableMock.Setup(static t => t.GetInformation()).Returns(_infoMock.Object);
        Mock<IEntity> entityMock = new();

        // Execute actual operation
        ICommand command = _tableMock.Object.Update(entityMock.Object);

        // Verify actual result
        Assert.NotNull(command);
        _ = Assert.IsType<UpdateJsonCommand<IEntity>>(command);
    }
}
