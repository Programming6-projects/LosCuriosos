namespace DistributionCenter.Application.Tests.Contexts.Bases;

using DistributionCenter.Application.Contexts.Bases;
using DistributionCenter.Application.Tables.Core.Interfaces;
using DistributionCenter.Domain.Entities.Interfaces;

public class BaseContextTests
{
    private readonly Mock<ITable<IEntity>> _tableMock;
    private readonly Mock<BaseContext> _context;

    public BaseContextTests()
    {
        _tableMock = new Mock<ITable<IEntity>>();

        Dictionary<Type, object> tables = new() { { typeof(IEntity), _tableMock.Object } };

        _context = new Mock<BaseContext>(tables) { CallBase = true };
    }

    [Fact]
    public void SetTable_ReturnsCorrectTable()
    {
        // Define input and Output
        ITable<IEntity> table = _context.Object.SetTable<IEntity>();

        // Verify actual result
        Assert.Equal(_tableMock.Object, table);
    }
}
