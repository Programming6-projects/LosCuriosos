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
        Guid? routeId = entity.RouteId;
        Guid clientId = entity.ClientId;
        Guid deliveryPointId = entity.DeliveryPointId;

        // Verify actual result
        Assert.Equal(expectedRouteId, routeId);
        Assert.Equal(expectedClientId, clientId);
        Assert.Equal(expectedDeliveryPointId, deliveryPointId);
    }

    [Fact]
    public void Equals_SameProperties_ReturnsTrue()
    {
        // Define Input and output
        Guid clientId = new();
        Guid routeId = new();
        Guid deliveryPointId = new();
        Status status = Status.Pending;
        Order order1 =
            new()
            {
                ClientId = clientId,
                RouteId = routeId,
                DeliveryPointId = deliveryPointId,
                Status = status,
            };
        Order order2 =
            new()
            {
                ClientId = clientId,
                RouteId = routeId,
                DeliveryPointId = deliveryPointId,
                Status = status,
            };

        // Execute actual operation
        bool result = order1.Equals(order2);

        // Verify actual result
        Assert.True(result);
    }

    [Fact]
    public void Equals_DifferentProperties_ReturnsFalse()
    {
        // Define Input and output
        Order order1 =
            new()
            {
                ClientId = new(),
                RouteId = new(),
                DeliveryPointId = new(),
                Status = Status.Pending,
            };
        Order order2 =
            new()
            {
                ClientId = new(),
                RouteId = new(),
                DeliveryPointId = new(),
                Status = Status.Shipped,
            };

        // Execute actual operation
        bool result = order1.Equals(order2);

        // Verify actual result
        Assert.False(result);
    }

    [Fact]
    public void GetHashCode_SameProperties_ReturnsSameHashCode()
    {
        // Define Input and output
        Guid clientId = new();
        Guid routeId = new();
        Guid deliveryPointId = new();
        Status status = Status.Pending;
        Order order1 =
            new()
            {
                ClientId = clientId,
                RouteId = routeId,
                DeliveryPointId = deliveryPointId,
                Status = status,
            };
        Order order2 =
            new()
            {
                ClientId = clientId,
                RouteId = routeId,
                DeliveryPointId = deliveryPointId,
                Status = status,
            };

        // Execute actual operation
        int hashCode1 = order1.GetHashCode();
        int hashCode2 = order2.GetHashCode();

        // Verify actual result
        Assert.Equal(hashCode1, hashCode2);
    }

    [Fact]
    public void Equals_DifferentObjects_ReturnFalse()
    {
        // Define Input and output
        Order order1 =
            new()
            {
                ClientId = new(),
                RouteId = new(),
                DeliveryPointId = new(),
                Status = Status.Pending,
            };

        // Execute actual operation
        bool result = order1.Equals(new object());

        // Verify actual result
        Assert.False(result);
    }

    [Fact]
    public void GetHashCode_DifferentProperties_ReturnsDifferentHashCode()
    {
        // Define Input and output
        Order order1 =
            new()
            {
                ClientId = new(),
                RouteId = new(),
                DeliveryPointId = new(),
                Status = Status.Pending,
            };
        Order order2 =
            new()
            {
                ClientId = new(),
                RouteId = new(),
                DeliveryPointId = new(),
                Status = Status.Shipped,
            };

        // Execute actual operation
        int hashCode1 = order1.GetHashCode();
        int hashCode2 = order2.GetHashCode();

        // Verify actual result
        Assert.NotEqual(hashCode1, hashCode2);
    }
}
