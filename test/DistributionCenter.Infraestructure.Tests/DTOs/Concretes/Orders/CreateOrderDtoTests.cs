namespace DistributionCenter.Infraestructure.Tests.DTOs.Concretes.Orders;

using Commons.Results;
using Domain.Entities.Concretes;
using Infraestructure.DTOs.Concretes.OrderProducts;
using Infraestructure.DTOs.Concretes.Orders;

public class CreateOrderDtoTests
{
    [Fact]
    public void ToEntity_ReturnsCorrectOrder()
    {
        // Define Input and Output
        CreateOrderDto dto =
            new()
            {
                ClientId = new Guid(),
                DeliveryPointId = new Guid(),
                Products = new List<CreateOrderProductDto>
                {
                    new () { ProductId = Guid.NewGuid(), Quantity = 2 }
                },
            };

        // Execute actual operation
        Order client = dto.ToEntity();

        // Verify actual result
        Assert.Equal(dto.ClientId, client.ClientId);
        Assert.Equal(dto.DeliveryPointId, client.DeliveryPointId);
    }

    [Fact]
    public void ToEntity_ShouldCreateOrderWithProducts()
    {
        Guid product1Id = Guid.NewGuid();
        Guid product2Id = Guid.NewGuid();
        CreateOrderDto dto = new ()
        {
            ClientId = Guid.NewGuid(),
            DeliveryPointId = Guid.NewGuid(),
            Products = new List<CreateOrderProductDto>
            {
                new () { ProductId = product1Id, Quantity = 2 },
                new () { ProductId = product2Id, Quantity = 3 }
            }
        };

        Order order = dto.ToEntity();

        // Assert
        Assert.Equal(2, order.Products.Count());
        Assert.Contains(order.Products, p => p.ProductId == product1Id && p.Quantity == 2);
        Assert.Contains(order.Products, p => p.ProductId == product2Id && p.Quantity == 3);
    }

    [Fact]
    public void ToEntity_ShouldAssignCorrectOrderIdToProducts()
    {
        CreateOrderDto dto = new ()
        {
            ClientId = Guid.NewGuid(),
            DeliveryPointId = Guid.NewGuid(),
            Products = new List<CreateOrderProductDto>
            {
                new () { ProductId = Guid.NewGuid(), Quantity = 2 }
            }
        };

        Order order = dto.ToEntity();

        foreach (OrderProduct product in order.Products)
        {
            Assert.Equal(order.Id, product.OrderId);
        }
    }

    [Fact]
    public void Validate_ShouldReturnValidResult_WhenAllFieldsAreValid()
    {
        CreateOrderDto dto = new ()
        {
            ClientId = Guid.NewGuid(),
            DeliveryPointId = Guid.NewGuid(),
            Products = new List<CreateOrderProductDto>
            {
                new () { ProductId = Guid.NewGuid(), Quantity = 2 }
            }
        };

        Result result = dto.Validate();

        // Assert
        Assert.True(result.IsSuccess);
    }
}
