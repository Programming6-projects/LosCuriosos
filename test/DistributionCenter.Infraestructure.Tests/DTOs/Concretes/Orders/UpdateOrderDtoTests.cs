namespace DistributionCenter.Infraestructure.Tests.DTOs.Concretes.Orders;

using Domain.Entities.Concretes;
using Domain.Entities.Enums;
using Infraestructure.DTOs.Concretes.Orders;

public class UpdateOrderDtoTests
{
    [Fact]
    public void ToEntity_ReturnsCorrectOrder()
    {
        Order existingOrder = new()
        {
            ClientId = Guid.NewGuid(),
            DeliveryPointId = Guid.NewGuid(),
            Products = new List<OrderProduct>()
        };

        UpdateOrderDto dto = new()
        {
            Status = Status.Pending,
            OrderProducts = new List<UpdateOrderProductDto>()
        };

        Order updatedOrder = dto.FromEntity(existingOrder);

        Assert.Equal(existingOrder.ClientId, updatedOrder.ClientId);
        Assert.Equal(existingOrder.DeliveryPointId, updatedOrder.DeliveryPointId);
        Assert.Equal(existingOrder.Products.Count, updatedOrder.Products.Count);
        Assert.Equal(dto.Status, updatedOrder.Status);
    }

    [Fact]
    public void ToEntity_OrderProductsShouldNotChange_WhenOrderProductsIsNull()
    {
        List<OrderProduct> existingProducts = new()
        {
            new OrderProduct
            {
                ProductId = Guid.NewGuid(),
                OrderId = Guid.NewGuid(),
                Quantity = 1,
                Product = new Product
                {
                    Name = "Pepsi",
                    Description = "The best drink",
                    Weight = 100
                }
            }
        };

        Order existingOrder = new()
        {
            Products = existingProducts,
            ClientId = Guid.NewGuid(),
            DeliveryPointId = Guid.NewGuid()
        };

        UpdateOrderDto dto = new()
        {
            Status = null,
            OrderProducts = null
        };

        Order updatedOrder = dto.FromEntity(existingOrder);

        // Assert
        Assert.Equal(existingProducts, updatedOrder.Products);
    }

    [Fact]
    public void ToEntity_ShouldUpdateOnlyNonNullFields()
    {
        Guid orderId = Guid.NewGuid();
        List<OrderProduct> existingProducts = new()
        {
            new OrderProduct
            {
                ProductId = Guid.NewGuid(),
                OrderId = orderId,
                Quantity = 1,
                Product = new Product
                {
                    Name = "Pepsi",
                    Description = "The best drink",
                    Weight = 100
                }
            }
        };

        Order existingOrder = new()
        {
            Status = Status.Pending,
            Products = existingProducts,
            ClientId = Guid.NewGuid(),
            DeliveryPointId = Guid.NewGuid()
        };

        List<UpdateOrderProductDto> newProducts = new()
        {
            new UpdateOrderProductDto
            {
                OrderProductId = orderId
            }
        };

        UpdateOrderDto dto = new()
        {
            Status = Status.Shipped,
            OrderProducts = newProducts
        };

        Order updatedOrder = dto.FromEntity(existingOrder);

        // Assert
        Assert.Equal(Status.Shipped, updatedOrder.Status);

        Assert.Equal(existingProducts.Count, updatedOrder.Products.Count);

        for (int i = 0; i < existingProducts.Count; i++)
        {
            Assert.Equal(existingProducts[i].ProductId, updatedOrder.Products[i].ProductId);
            Assert.Equal(existingProducts[i].Quantity, updatedOrder.Products[i].Quantity);
            Assert.Equal(existingProducts[i].Product.Name, updatedOrder.Products[i].Product.Name);
            Assert.Equal(existingProducts[i].Product.Description, updatedOrder.Products[i].Product.Description);
            Assert.Equal(existingProducts[i].Product.Weight, updatedOrder.Products[i].Product.Weight);
        }
    }
}
