namespace DistributionCenter.Infraestructure.Tests.DTOs.Concretes.Orders;

using Domain.Entities.Concretes;
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
                RouteId = new Guid(),
                ClientId = new Guid(),
                Status = "Pending",
                DeliveryPointId = new Guid(),
            };

        // Execute actual operation
        Order client = dto.ToEntity();

        // Verify actual result
        Assert.Equal(dto.RouteId, client.RouteId);
        Assert.Equal(dto.ClientId, client.ClientId);
        Assert.Equal(dto.DeliveryPointId, client.DeliveryPointId);
    }
}
