namespace DistributionCenter.Domain.Tests.Entities.Concretes;

using Domain.Entities.Concretes;
using Domain.Entities.Enums;

public class TripTests
{
    [Fact]
    public void Test_Trip()
    {
        // Define Input and output
        Status expectedStatus = Status.Pending;
        Guid expectedTransportId = new();
        Trip entity =
            new()
            {
                Status = expectedStatus,
                TransportId = expectedTransportId,
            };

        // Execute actual operation
        Status status = entity.Status;
        Guid transportId = new();

        // Verify actual result
        Assert.Equal(expectedStatus, status);
        Assert.Equal(expectedTransportId, transportId);
    }
}
