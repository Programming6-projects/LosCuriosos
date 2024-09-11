namespace DistributionCenter.Application.Tests.Tables.Components.QueryCommands.Concretes.Dapper.Bases;

using System.Data;
using DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.Dapper.Bases;
using DistributionCenter.Application.Tables.Connections.Interfaces;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;
using Moq.Protected;

public class BaseDapperCommandTests
{
    private readonly Mock<BaseDapperCommand<IEntity>> _baseDapperCommand;
    private readonly Mock<IDbConnectionFactory> _dbConnectionFactory;
    private readonly Mock<IDbConnection> _dbConnection;

    public BaseDapperCommandTests()
    {
        _dbConnectionFactory = new Mock<IDbConnectionFactory>();
        _dbConnection = new Mock<IDbConnection>();
        _baseDapperCommand = new Mock<BaseDapperCommand<IEntity>>(
            _dbConnectionFactory.Object,
            new Mock<IEntity>().Object,
            "test"
        )
        {
            CallBase = true,
        };
    }

    [Fact]
    public async Task ExecuteAsync_CallsCreateConnectionAsyncAndExecuteAsync()
    {
        // Define Input and Output
        _ = _dbConnectionFactory.Setup(static m => m.CreateConnectionAsync()).ReturnsAsync(_dbConnection.Object);
        _ = _baseDapperCommand
            .Protected()
            .Setup<Task<Result>>("Execute", ItExpr.IsAny<IDbConnection>())
            .ReturnsAsync(Result.Ok());

        // Execute actual operation
        Result result = await _baseDapperCommand.Object.ExecuteAsync();

        // Verify actual result
        _dbConnectionFactory.Verify(static m => m.CreateConnectionAsync(), Times.Once);
        _baseDapperCommand.Protected().Verify("Execute", Times.Once(), ItExpr.IsAny<IDbConnection>());

        Assert.True(result.IsSuccess);
    }
}
