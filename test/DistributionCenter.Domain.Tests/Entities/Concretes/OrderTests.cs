namespace DistributionCenter.Domain.Tests.Entities.Concretes;

using Domain.Entities.Concretes;

public class OrderTests
{
    [Fact]
    public void Test_Order()
    {
        // Define Input and output
        Guid expectedClientId = new();
        Guid expectedOrderStatusId = new();
        Order entity =
            new()
            {
                ClientId = expectedClientId,
                OrderStatusId = expectedOrderStatusId,
            };

        // Execute actual operation
        Guid clientId = entity.ClientId;
        Guid orderStatusId = entity.OrderStatusId;

        // Verify actual result
        Assert.Equal(expectedClientId, clientId);
        Assert.Equal(expectedOrderStatusId, orderStatusId);
    }
}
