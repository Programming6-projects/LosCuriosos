namespace DistributionCenter.Application.Tests.Tables.Core.Concretes;

using System.Data;
using Application.Tables.Components.Information.Concretes;
using Application.Tables.Components.Information.Interfaces;
using Application.Tables.Connections.Dapper.Interfaces;
using Application.Tables.Core.Concretes;

public class OrderTableTests
{
    [Fact]
    public void GetInformation_ShouldReturnOrderTableInformation()
    {
        // Define Input and Output
        Mock<IDbConnectionFactory<IDbConnection>> mockFactory = new();
        OrderTable table = new(mockFactory.Object);

        // Execute actual operation
        ITableInformation info = table.GetInformation();

        // Verify actual result
        _ = Assert.IsType<OrderTableInformation>(info);
    }
}
