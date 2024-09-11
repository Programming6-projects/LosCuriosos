namespace DistributionCenter.Application.Tests.Tables.Components.QueryCommands.Concretes.Dapper.Bases;

using System.Collections.ObjectModel;
using System.Data;
using DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.Dapper.Bases;
using DistributionCenter.Application.Tables.Connections.Interfaces;
using DistributionCenter.Commons.Errors;
using DistributionCenter.Commons.Errors.Interfaces;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;
using Moq.Protected;

public class BaseDapperQueryTests
{
    private readonly Mock<BaseDapperQuery<IEntity>> _dapperQuery;
    private readonly Mock<IDbConnectionFactory> _dbConnectionFactory;
    private readonly Mock<IDbConnection> _dbConnection;

    public BaseDapperQueryTests()
    {
        _dbConnectionFactory = new Mock<IDbConnectionFactory>();
        _dbConnection = new Mock<IDbConnection>();
        _dapperQuery = new Mock<BaseDapperQuery<IEntity>>(_dbConnectionFactory.Object, "test") { CallBase = true };
    }

    [Fact]
    public async Task ExecuteAsync_CallsCreateConnectionAsyncAndExecuteAsync()
    {
        // Define Input and Output
        Result<IEntity> expectedResult = new(new Mock<IEntity>().Object);

        _ = _dbConnectionFactory.Setup(static m => m.CreateConnectionAsync()).ReturnsAsync(_dbConnection.Object);
        _ = _dapperQuery
            .Protected()
            .Setup<Task<Result<IEntity>>>("Execute", ItExpr.IsAny<IDbConnection>())
            .ReturnsAsync(expectedResult);

        // Execute actual operation
        Result<IEntity> result = await _dapperQuery.Object.ExecuteAsync();

        // Verify actual result
        _dbConnectionFactory.Verify(static m => m.CreateConnectionAsync(), Times.Once);
        _dapperQuery.Protected().Verify("Execute", Times.Once(), ItExpr.IsAny<IDbConnection>());

        Assert.True(result.IsSuccess);
        Assert.Equal(expectedResult.Value, result.Value);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsErrorResult()
    {
        // Define Input and Output
        Collection<IError> errors = [Error.Conflict()];
        Result<IEntity> expectedResult = errors;

        _ = _dbConnectionFactory.Setup(static m => m.CreateConnectionAsync()).ReturnsAsync(_dbConnection.Object);
        _ = _dapperQuery
            .Protected()
            .Setup<Task<Result<IEntity>>>("Execute", ItExpr.IsAny<IDbConnection>())
            .ReturnsAsync(expectedResult);

        // Execute actual operation
        Result<IEntity> result = await _dapperQuery.Object.ExecuteAsync();

        // Verify actual result
        _dbConnectionFactory.Verify(static m => m.CreateConnectionAsync(), Times.Once);
        _dapperQuery.Protected().Verify("Execute", Times.Once(), ItExpr.IsAny<IDbConnection>());

        Assert.False(result.IsSuccess);
        Assert.Equal(expectedResult.Errors, result.Errors);
    }
}
