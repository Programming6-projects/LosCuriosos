namespace DistributionCenter.Application.Tests.Tables.Core.Concretes;

using Application.Tables.Components.Information.Concretes;
using Application.Tables.Components.Information.Interfaces;
using Application.Tables.Connections.Interfaces;
using Application.Tables.Core.Concretes;
using Domain.Entities.Concretes;

public class TransportTableTests
{
    [Fact]
    public void GetInformation_ShouldReturnTransportTableInformation()
    {
        // Arrange: Setup the mock for the file connection factory
        Mock<IFileConnectionFactory<Transport>> mockFactory = new();
        TransportTable table = new(mockFactory.Object);

        // Act: Execute the method being tested
        ITableInformation info = table.GetInformation();

        // Assert: Verify the type of the returned information
        _ = Assert.IsType<TransportTableInformation>(info);
    }
}
