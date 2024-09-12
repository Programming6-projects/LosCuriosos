namespace DistributionCenter.Infraestructure.Tests.DTOs.Concretes.Orders;

using Domain.Entities.Concretes;
using Infraestructure.DTOs.Concretes.Orders;

public class UpdateOrderDtoTests
{
    [Fact]
    public void FromEntity_UpdatesAndReturnsCorrectOrder()
    {
        // Define Input and Output
        Order order =
            new()
            {
                ClientId = Guid.NewGuid(),
                OrderStatusId = Guid.NewGuid(),
            };
        UpdateOrderDto dto =
            new()
            {
                ClientId = Guid.NewGuid(),
                OrderStatusId = Guid.NewGuid(),
            };

        // Execute actual operation
        Order updatedOrder = dto.FromEntity(order);

        // Verify actual result
        Assert.Equal(dto.ClientId, updatedOrder.ClientId);
        Assert.Equal(dto.OrderStatusId, updatedOrder.OrderStatusId);
    }

    [Fact]
    public void FromEntity_UpdatesWithNullsAndReturnsCorrectOrder()
    {
        // Define Input and Output
        Guid initialClientId = Guid.NewGuid();
        Guid initialOrderStatusId = Guid.NewGuid();
        Order order =
            new()
            {
                ClientId = initialClientId,
                OrderStatusId = initialOrderStatusId,
            };
        UpdateOrderDto dto = new();

        // Execute actual operation
        Order updatedOrder = dto.FromEntity(order);

        // Verify actual result
        Assert.Equal(order.ClientId, updatedOrder.ClientId);
        Assert.Equal(order.OrderStatusId, updatedOrder.OrderStatusId);
    }
}
