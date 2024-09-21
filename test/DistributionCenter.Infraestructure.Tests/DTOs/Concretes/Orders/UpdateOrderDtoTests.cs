namespace DistributionCenter.Infraestructure.Tests.DTOs.Concretes.Orders;

using Domain.Entities.Concretes;
using Domain.Entities.Enums;
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
        Assert.Equal(dto.ClientId, updatedOrder.ClientId);
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
}
