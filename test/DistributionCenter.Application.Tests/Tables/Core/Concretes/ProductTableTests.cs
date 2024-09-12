namespace DistributionCenter.Application.Tests.Tables.Core.Concretes;

using Application.Tables.Components.Information.Interfaces;
using Application.Tables.Connections.Interfaces;

public class ProductTableTests
{
    [Fact]
    public void GetInformation_ShouldReturnProductTableInformation()
    {
        // Define Input and Output
        Mock<IDbConnectionFactory> mockFactory = new();
        ProductTable table = new(mockFactory.Object);

        // Execute actual operation
        ITableInformation info = table.GetInformation();

        // Verify actual result
        _ = Assert.IsType<ProductTableInformation>(info);
    }
}
