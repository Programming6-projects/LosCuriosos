namespace DistributionCenter.Domain.Tests.Entities.Concretes;

using Domain.Entities.Concretes;

public class OrderProductTests
{
    [Fact]
    public void Test_OrderProduct()
    {
        // Define Input and output
        Guid expectedProductId = new();
        Guid expectedOrderId = new();
        int expectedQuantity = 5;
        Product expectedProduct =
            new()
            {
                Name = "Product",
                Weight = 5,
                Description = "Description",
            };
        OrderProduct entity =
            new()
            {
                ProductId = expectedProductId,
                OrderId = expectedOrderId,
                Quantity = expectedQuantity,
                Product = expectedProduct,
            };

        // Execute actual operation
        Guid productId = entity.ProductId;
        Guid orderId = entity.OrderId;
        int quantity = entity.Quantity;
        Product product = entity.Product;

        // Verify actual result
        Assert.Equal(expectedProductId, productId);
        Assert.Equal(expectedOrderId, orderId);
        Assert.Equal(expectedQuantity, quantity);
        Assert.Equal(expectedProduct, product);
    }
}
