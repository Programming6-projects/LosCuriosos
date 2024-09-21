namespace DistributionCenter.Domain.Tests.Entities.Concretes;

using Domain.Entities.Concretes;

public class OrderTests
{
    [Fact]
    public void Test_Order()
    {
        // Define Input and output
        Guid expectedRouteId = new();
        Guid expectedClientId = new();
        Guid expectedDeliveryPointId = new();
        Order entity =
            new()
            {
                RouteId = expectedRouteId,
                ClientId = expectedClientId,
                DeliveryPointId = expectedDeliveryPointId,
                Status = Status.Pending,
            };

        // Execute actual operation
        Guid routeId = entity.RouteId;
        Guid clientId = entity.ClientId;
        Guid deliveryPointId = entity.DeliveryPointId;

        // Verify actual result
        Assert.Equal(expectedRouteId, routeId);
        Assert.Equal(expectedClientId, clientId);
        Assert.Equal(expectedDeliveryPointId, deliveryPointId);
    }
}
