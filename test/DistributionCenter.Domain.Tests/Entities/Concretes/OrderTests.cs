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
        Guid routeId = entity.RouteId;
        Guid clientId = entity.ClientId;
        Status orderStatus = entity.Status;
        IReadOnlyList<OrderProduct> clientOrderProducts = entity.Products;

        // Verify actual result
        Assert.Equal(expectedRouteId, routeId);
        Assert.Equal(expectedClientId, clientId);
        Assert.Equal(Status.Pending, orderStatus);
        _ = Assert.Single(clientOrderProducts);
        Assert.Equal(expectedProductId, clientOrderProducts[0].ProductId);
    }
}
