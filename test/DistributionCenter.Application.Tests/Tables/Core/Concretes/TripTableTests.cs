namespace DistributionCenter.Application.Tests.Tables.Core.Concretes;

using System.Data;
using Application.Tables.Components.Information.Interfaces;
using Application.Tables.Connections.Dapper.Interfaces;

public class TripTableTests
{
    [Fact]
    public void GetInformation_ShouldReturnClientTableInformation()
    {
        // Define Input and Output
        Mock<IDbConnectionFactory<IDbConnection>> mockFactory = new();
        TripTable table = new(mockFactory.Object);

        // Execute actual operation
        ITableInformation info = table.GetInformation();

        // Verify actual result
        _ = Assert.IsType<TripTableInformation>(info);
    }
}
