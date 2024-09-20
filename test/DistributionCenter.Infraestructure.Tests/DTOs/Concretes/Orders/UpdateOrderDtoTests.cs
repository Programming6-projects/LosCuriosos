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
                RouteId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                DeliveryPointId = Guid.NewGuid(),
            };
        UpdateOrderDto dto = new() { ClientId = Guid.NewGuid() };

        // Execute actual operation
        Order updatedOrder = dto.FromEntity(order);

        // Verify actual result
        Assert.Equal(dto.ClientId, updatedOrder.ClientId);
    }

    [Fact]
    public void FromEntity_UpdatesWithNullsAndReturnsCorrectOrder()
    {
        // Define Input and Output
        Guid initialRouteId = Guid.NewGuid();
        Guid initialClientId = Guid.NewGuid();
        Guid initialDeliveryPointId = Guid.NewGuid();
        Order order =
            new()
            {
                RouteId = initialRouteId,
                ClientId = initialClientId,
                DeliveryPointId = initialDeliveryPointId,
            };
        UpdateOrderDto dto = new();

        // Execute actual operation
        Order updatedOrder = dto.FromEntity(order);

        // Verify actual result
        Assert.Equal(order.RouteId, updatedOrder.RouteId);
        Assert.Equal(order.ClientId, updatedOrder.ClientId);
        Assert.Equal(order.DeliveryPointId, updatedOrder.DeliveryPointId);
    }
}
