namespace DistributionCenter.Domain.Tests.Entities.Concretes;

using DistributionCenter.Domain.Entities.Enums;
using Domain.Entities.Concretes;

public class OrderTests
{
    [Fact]
    public void Test_Order()
    {
        // Define Input and output
        Guid expectedClientId = Guid.NewGuid();
        Guid expectedDeliveredPointId = Guid.NewGuid();
        Guid expectedRouteId = Guid.NewGuid();
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

        Order entity =
            new()
            {
                Status = Status.Pending,
                RouteId = expectedRouteId,
                DeliveryPointId = expectedDeliveredPointId,
                Products = expectedClientOrderProduct,
                ClientId = expectedClientId,
            };

        // Execute actual operation
        Guid clientId = entity.ClientId;
        Status orderStatus = entity.Status;
        IReadOnlyList<OrderProduct> clientOrderProducts = entity.Products;

        // Verify actual result
        Assert.Equal(expectedClientId, clientId);
        Assert.Equal(Status.Pending, orderStatus);
        _ = Assert.Single(clientOrderProducts);
        Assert.Equal(expectedProductId, clientOrderProducts[0].ProductId);
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
                Products = new List<OrderProduct>()
            };
        Order order2 =
            new()
            {
                ClientId = new(),
                RouteId = new(),
                DeliveryPointId = new(),
                Status = Status.Shipped,
                Products = new List<OrderProduct>()
            };

        // Execute actual operation
        bool result = order1.Equals(order2);

        // Verify actual result
        Assert.False(result);
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
                Products = new List<OrderProduct>()
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
                Products = new List<OrderProduct>()
            };
        Order order2 =
            new()
            {
                ClientId = new(),
                RouteId = new(),
                DeliveryPointId = new(),
                Status = Status.Shipped,
                Products = new List<OrderProduct>()
            };

        // Execute actual operation
        int hashCode1 = order1.GetHashCode();
        int hashCode2 = order2.GetHashCode();

        // Verify actual result
        Assert.NotEqual(hashCode1, hashCode2);
    }
}
