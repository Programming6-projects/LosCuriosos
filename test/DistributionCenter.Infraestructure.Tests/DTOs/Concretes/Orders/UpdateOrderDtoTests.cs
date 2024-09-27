namespace DistributionCenter.Infraestructure.Tests.DTOs.Concretes.Orders;

using Commons.Results;
using Domain.Entities.Concretes;
using Domain.Entities.Enums;
using Infraestructure.DTOs.Concretes.OrderProducts;
using Infraestructure.DTOs.Concretes.Orders;

public class UpdateOrderDtoTests
{
    [Fact]
    public void FromEntity_UpdatesAndReturnsCorrectOrder()
    {
        // Define Input and Output
        Guid clientId = Guid.NewGuid();
        Order order =
            new()
            {
                RouteId = Guid.NewGuid(),
                ClientId = clientId,
                DeliveryPointId = Guid.NewGuid(),
                Status = Status.Pending,
            };
        UpdateOrderDto dto =
            new()
            {
                Status = "Cancelled",
            };

        // Execute actual operation
        Order updatedOrder = dto.FromEntity(order);

        // Verify actual result
        _ = Enum.TryParse(dto.Status, true, out Status status);
        Assert.Equal(status, updatedOrder.Status);
        Assert.Equal(clientId, updatedOrder.ClientId);
    }

    [Fact]
    public void FromEntity_UpdatesWithNullsAndReturnsCorrectOrder()
    {
        // Define Input and Output
        Guid initialRouteId = Guid.NewGuid();
        Guid initialClientId = Guid.NewGuid();
        Guid initialDeliveryPointId = Guid.NewGuid();
        Status status = Status.Pending;
        Order order =
            new()
            {
                RouteId = initialRouteId,
                ClientId = initialClientId,
                DeliveryPointId = initialDeliveryPointId,
                Status = status,
            };
        UpdateOrderDto dto = new();

        // Execute actual operation
        Order updatedOrder = dto.FromEntity(order);

        // Verify actual result
        Assert.Equal(order.RouteId, updatedOrder.RouteId);
        Assert.Equal(order.ClientId, updatedOrder.ClientId);
        Assert.Equal(order.Status, updatedOrder.Status);
        Assert.Equal(order.DeliveryPointId, updatedOrder.DeliveryPointId);
    }

    [Fact]
    public void FromEntity_ShouldUpdateOrderStatus_WhenStatusIsValid()
    {
        Order order = new ()
        {
            Status = Status.Pending,
            ClientId = Guid.NewGuid(),
            DeliveryPointId = Guid.NewGuid()
        };
        UpdateOrderDto dto = new () { Status = "Shipped" };

        Order updatedOrder = dto.FromEntity(order);

        // Assert
        Assert.Equal(Status.Shipped, updatedOrder.Status);
    }

    [Fact]
    public void FromEntity_ShouldNotUpdateStatus_WhenStatusIsNull()
    {
        Status originalStatus = Status.Pending;
        Order order = new ()
        {
            Status = originalStatus,
            ClientId = Guid.NewGuid(),
            DeliveryPointId = Guid.NewGuid()
        };
        UpdateOrderDto dto = new () { Status = null };

        Order updatedOrder = dto.FromEntity(order);

        // Assert
        Assert.Equal(originalStatus, updatedOrder.Status);
    }

    [Fact]
    public void FromEntity_ShouldUpdateProductQuantities_WhenProductExistsInOrder()
    {
        Guid productId1 = Guid.NewGuid();
        Guid productId2 = Guid.NewGuid();
        Order order = new ()
        {
            Products = new List<OrderProduct>
            {
                new ()
                {
                    ProductId = productId1,
                    Quantity = 2
                },
                new ()
                {
                    ProductId = productId2,
                    Quantity = 3
                }
            },
            ClientId = default,
            DeliveryPointId = default
        };
        UpdateOrderDto dto = new ()
        {
            Products = new List<UpdateOrderProductDto>
            {
                new () { ProductId = productId1, Quantity = 5 }
            }
        };

        Order updatedOrder = dto.FromEntity(order);

        // Assert
        OrderProduct updatedProduct = updatedOrder.Products.First(p => p.ProductId == productId1);
        Assert.Equal(5, updatedProduct.Quantity);
    }

    [Fact]
    public void FromEntity_ShouldNotUpdateProductQuantity_WhenProductDoesNotExistInOrder()
    {
        Guid productId1 = Guid.NewGuid();
        Guid productIdNotInOrder = Guid.NewGuid();
        Order order = new ()
        {
            Products = new List<OrderProduct>
            {
                new()
                {
                    ProductId = productId1,
                    Quantity = 2
                }
            },
            ClientId = Guid.NewGuid(),
            DeliveryPointId = Guid.NewGuid()
        };
        UpdateOrderDto dto = new ()
        {
            Products = new List<UpdateOrderProductDto>
            {
                new () { ProductId = productIdNotInOrder, Quantity = 5 }
            }
        };

        Order updatedOrder = dto.FromEntity(order);

        // Assert
        OrderProduct originalProduct = updatedOrder.Products.First(p => p.ProductId == productId1);
        Assert.Equal(2, originalProduct.Quantity);
    }

    [Fact]
    public void Validate_ShouldReturnSuccessResult()
    {
        UpdateOrderDto dto = new ()
        {
            Status = "Shipped",
            Products = new List<UpdateOrderProductDto>
            {
                new () { ProductId = Guid.NewGuid(), Quantity = 1 }
            }
        };

        Result result = dto.Validate();

        // Assert
        Assert.True(result.IsSuccess);
    }
}
