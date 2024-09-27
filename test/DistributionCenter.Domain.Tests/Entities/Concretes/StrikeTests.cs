namespace DistributionCenter.Domain.Tests.Entities.Concretes;

public class StrikeTests
{
    [Fact]
    public void Test_Strike()
    {
        // Define Input and output
        string expectedDescription = "Description";
        Guid expectedTransportId = new();

        Strike entity = new() { Description = expectedDescription, TransportId = expectedTransportId };

        // Execute actual operation
        string description = entity.Description;
        Guid transportId = entity.TransportId;

        // Verify actual result
        Assert.Equal(expectedDescription, description);
        Assert.Equal(expectedTransportId, transportId);
    }
}
