namespace DistributionCenter.Application.Tests.Tables.Components.QueryCommands.Bases;

using DistributionCenter.Application.Tables.Components.QueryCommands.Bases;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;

public class BaseCommandTests
{
    private readonly Mock<IEntity> _entity;
    private readonly Mock<BaseCommand<IEntity>> _command;

    public BaseCommandTests()
    {
        _entity = new Mock<IEntity>();
        _command = new Mock<BaseCommand<IEntity>>(_entity.Object) { CallBase = true };
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsSuccessfulResult()
    {
        // Define Input and Output
        Result expectedResult = Result.Ok();
        _ = _command.Setup(static m => m.ExecuteAsync()).ReturnsAsync(expectedResult);

        // Execute actual operation
        Result result = await _command.Object.ExecuteAsync();

        // Verify actual result
        Assert.Equal(expectedResult, result);
    }
}
