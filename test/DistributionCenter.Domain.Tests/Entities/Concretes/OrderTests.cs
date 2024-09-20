namespace DistributionCenter.Domain.Tests.Entities.Concretes;

using Commons.Enums;
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
            Description = "La mejor bebida",
            Weight = 100,
        };

        ClientOrderProduct clientOrderProduct =
            new ()
            {
                ProductId = expectedProductId,
                OrderId = expectedClientId,
                Quantity = 0,
                Product = product
            };

        IReadOnlyList<ClientOrderProduct> expectedClientOrderProduct = new List<ClientOrderProduct> { clientOrderProduct };

        Order entity =
            new()
            {
                Status = Status.Pending,
                RouteId = expectedRouteId,
                DeliveryPointId = expectedDeliveredPointId,
                ClientOrderProducts = expectedClientOrderProduct,
                ClientId = expectedClientId,
            };

        // Execute actual operation
        Guid clientId = entity.ClientId;
        Status orderStatus = entity.Status;
        IReadOnlyList<ClientOrderProduct> clientOrderProducts = entity.ClientOrderProducts;

        // Verify actual result
        Assert.Equal(expectedClientId, clientId);
        Assert.Equal(Status.Pending, orderStatus);
        _ = Assert.Single(clientOrderProducts);
        Assert.Equal(expectedProductId, clientOrderProducts[0].ProductId);
    }
}
