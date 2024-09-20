namespace DistributionCenter.Domain.Tests.Entities.Concretes;

using DistributionCenter.Domain.Entities.Enums;
using Domain.Entities.Concretes;

public class TripTests
{
    [Fact]
    public void Test_Trip()
    {
        // Define Input and output
        Guid expectedTransportId = new();
        Order expectedOrder =
            new()
            {
                RouteId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                DeliveryPointId = Guid.NewGuid(),
            };
        Trip entity = new() { TransportId = expectedTransportId, Orders = [expectedOrder] };

        // Execute actual operation
        Guid transportId = entity.TransportId;
        ICollection<Order> orders = entity.Orders;

        // Verify actual result
        Assert.Equal(expectedTransportId, transportId);
        Assert.Contains(expectedOrder, orders);
    }

    [Fact]
    public void Test_Trip_DefaultStatus()
    {
        // Arrange
        Trip entity = new() { TransportId = Guid.NewGuid() };

        // Act
        Status status = entity.Status;

        // Assert
        Assert.Equal(Status.Pending, status);
    }
}
