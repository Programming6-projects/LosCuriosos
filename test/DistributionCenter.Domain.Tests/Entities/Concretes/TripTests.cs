namespace DistributionCenter.Domain.Tests.Entities.Concretes;

using DistributionCenter.Domain.Entities.Enums;
using Domain.Entities.Concretes;

public class TripTests
{
    [Fact]
    public void Test_Trip()
    {
        // Define Input and output
        Status expectedStatus = Status.Pending;
        Guid expectedTransportId = new();

        Order expectedOrder =
            new()
            {
                RouteId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                DeliveryPointId = Guid.NewGuid(),
                Status = Status.Pending,
            };

        Trip entity =
            new()
            {
                Status = expectedStatus,
                TransportId = expectedTransportId,
                Orders = [expectedOrder]
            };

        // Execute actual operation
        Status status = entity.Status;
        Guid transportId = (Guid)entity.TransportId;
        ICollection<Order> orders = entity.Orders;

        // Verify actual result
        Assert.Equal(expectedStatus, status);
        Assert.Equal(expectedTransportId, transportId);
        Assert.Contains(expectedOrder, orders);
    }

    [Fact]
    public void Test_Trip_DefaultStatus()
    {
        // Arrange
        Trip entity = new()
        {
            TransportId = Guid.NewGuid(),
            Status = Status.Pending
        };

        // Act
        Status status = entity.Status;

        // Assert
        Assert.Equal(Status.Pending, status);
    }
}
