namespace DistributionCenter.Application.Tests.Tables.Core.Bases;

using System.Data;
using Application.Tables.Connections.Dapper.Interfaces;
using DistributionCenter.Application.Tables.Components.Information.Interfaces;
using DistributionCenter.Application.Tables.Components.QueryCommands.Interfaces;
using DistributionCenter.Application.Tables.Core.Bases;
using DistributionCenter.Domain.Entities.Interfaces;

public class BaseDapperTableTests
{
    private readonly Mock<IDbConnectionFactory<IDbConnection>> _factoryMock;
    private readonly Mock<BaseDapperTable<IEntity>> _tableMock;
    private readonly Mock<ITableInformation> _infoMock;

    public BaseDapperTableTests()
    {
        _factoryMock = new Mock<IDbConnectionFactory<IDbConnection>>();
        _tableMock = new Mock<BaseDapperTable<IEntity>>(_factoryMock.Object) { CallBase = true };
        _infoMock = new Mock<ITableInformation>();
    }

    [Fact]
    public void GetById_ShouldReturnQuery()
    {
        // Define Input and Output
        _ = _infoMock.Setup(static i => i.TableName).Returns("table");
        _ = _infoMock.Setup(static i => i.GetByIdFields).Returns("fields");
        _ = _tableMock.Setup(static t => t.GetInformation()).Returns(_infoMock.Object);

        // Execute actual operation
        IQuery<IEntity> query = _tableMock.Object.GetById(Guid.NewGuid());

        // Verify actual result
        Assert.NotNull(query);
    }

    [Fact]
    public void GetAll_ShouldReturnQuery()
    {
        // Define input and Output
        _ = _infoMock.Setup(static i => i.TableName).Returns("table");
        _ = _infoMock.Setup(static i => i.GetByIdFields).Returns("fields");
        _ = _tableMock.Setup(static t => t.GetInformation()).Returns(_infoMock.Object);

        // Execute actual operation
        IQuery<IEnumerable<IEntity>> query = _tableMock.Object.GetAll();

        // Verigy actual result
        Assert.NotNull(query);
    }

    [Fact]
    public void Create_ShouldReturnCommand()
    {
        // Define Input and Output
        _ = _infoMock.Setup(static i => i.TableName).Returns("table");
        _ = _infoMock.Setup(static i => i.CreateFields).Returns("fields");
        _ = _infoMock.Setup(static i => i.CreateValues).Returns("values");
        _ = _tableMock.Setup(static t => t.GetInformation()).Returns(_infoMock.Object);

        // Execute actual operation
        ICommand command = _tableMock.Object.Create(new Mock<IEntity>().Object);

        // Verify actual result
        Assert.NotNull(command);
    }

    [Fact]
    public void Update_ShouldReturnCommand()
    {
        // Define Input and Output
        _ = _infoMock.Setup(static i => i.TableName).Returns("table");
        _ = _infoMock.Setup(static i => i.UpdateFields).Returns("fields");
        _ = _tableMock.Setup(static t => t.GetInformation()).Returns(_infoMock.Object);

        // Execute actual operation
        ICommand command = _tableMock.Object.Update(new Mock<IEntity>().Object);

        // Verify actual result
        Assert.NotNull(command);
    }
}
