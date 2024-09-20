namespace DistributionCenter.Domain.Tests.Entities.Concretes;

using Domain.Entities.Concretes;
using Domain.Entities.Enums;

public class OrderTests
{
    [Fact]
    public void Test_Order()
    {
        // Define Input and output
        Guid expectedClientId = new();
        Status status = Status.Pending;
        Order entity =
            new()
            {
                ClientId = expectedClientId,
                Status = status,
            };

        // Execute actual operation
        Guid clientId = entity.ClientId;
        Status statusObtained = entity.Status;

        // Verify actual result
        Assert.Equal(expectedClientId, clientId);
        Assert.Equal(status, statusObtained);
    }
}
