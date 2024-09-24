namespace DistributionCenter.Domain.Tests.Entities.Concretes;

using Domain.Entities.Concretes;

public class TripTests
{
    [Fact]
    public void Test_Trip()
    {
        // Define Input and output
        Status expectedStatus = Status.Pending;
        Guid expectedTransportId = new();

        Guid expectedClientId = Guid.NewGuid();
        Guid expectedProductId = Guid.NewGuid();
        Product product = new()
        {
            Name = "Pepsi",
            Description = "The best drink",
            Weight = 100,
        };

        OrderProduct orderProduct =
            new ()
            {
                ProductId = expectedProductId,
                OrderId = expectedClientId,
                Quantity = 0,
                Product = product
            };

        IReadOnlyList<OrderProduct> expectedClientOrderProduct = new List<OrderProduct> { orderProduct };
        Order expectedOrder =
            new()
            {
                Status = Status.Pending,
                RouteId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                DeliveryPointId = Guid.NewGuid(),
                Products = expectedClientOrderProduct,
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
        Status status = (Status)entity.Status;

        // Assert
        Assert.Equal(Status.Pending, status);
    }
}
