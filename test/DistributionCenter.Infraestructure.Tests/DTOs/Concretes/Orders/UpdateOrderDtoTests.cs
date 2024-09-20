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
                ClientId = Guid.NewGuid(),
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
    }

    [Fact]
    public void FromEntity_UpdatesWithNullsAndReturnsCorrectOrder()
    {
        // Define Input and Output
        Guid initialClientId = Guid.NewGuid();
        Status status = Status.Pending;
        Order order =
            new()
            {
                ClientId = initialClientId,
                Status = status,
            };
        UpdateOrderDto dto = new();

        // Execute actual operation
        Order updatedOrder = dto.FromEntity(order);

        // Verify actual result
        Assert.Equal(order.ClientId, updatedOrder.ClientId);
        Assert.Equal(order.Status, updatedOrder.Status);
    }
}
