namespace DistributionCenter.Application.Tests.Tables.Core.Concretes;

using System.Data;
using Application.Tables.Connections.Dapper.Interfaces;
using DistributionCenter.Application.Tables.Components.Information.Concretes;
using DistributionCenter.Application.Tables.Components.Information.Interfaces;
using DistributionCenter.Application.Tables.Core.Concretes;
using DistributionCenter.Application.Tables.Connections.Interfaces;

public class ClientTableTests
{
    [Fact]
    public void GetInformation_ShouldReturnClientTableInformation()
    {
        // Define Input and Output
        Mock<IDbConnectionFactory<IDbConnection>> mockFactory = new();
        ClientTable table = new(mockFactory.Object);

        // Execute actual operation
        ITableInformation info = table.GetInformation();

        // Verify actual result
        _ = Assert.IsType<ClientTableInformation>(info);
    }
}
