namespace DistributionCenter.Infraestructure.Tests.DTOs.Concretes.Orders;

using Commons.Results;
using Domain.Entities.Concretes;
using Infraestructure.DTOs.Concretes.Orders;

public class UpdateOrderDtoTests
{
    [Fact]
    public void ToEntity_ReturnsCorrectOrder()
    {
        // Define Input and Output
        CreateOrderDto dto = new()
        {
            ClientId = Guid.NewGuid(),
            Status = "Pending",
            RouteId = Guid.NewGuid(),
            DeliveryPointId = Guid.NewGuid(),
            ClientOrderProducts = new List<OrderProduct>()
        };

        // Execute actual operation
        Order client = dto.ToEntity();

        // Verify actual result
        Assert.Equal(dto.ClientId, client.ClientId);
        Assert.Equal(dto.Status, client.Status.ToString());
        Assert.Equal(dto.RouteId, client.RouteId);
        Assert.Equal(dto.DeliveryPointId, client.DeliveryPointId);
        Assert.Equal(dto.ClientOrderProducts.Count, client.Products.Count);
    }

    [Fact]
    public void VerifyThatTheDataWasValidatedSuccessfully()
    {
        // Define Input and Output
        int expectedErrorsQuantity = 2;
        CreateOrderDto invalidDto = new()
        {
            ClientId = default,
            Status = "X",
            RouteId = default,
            DeliveryPointId = default,
            ClientOrderProducts = new List<OrderProduct>(),
        };

        CreateOrderDto validDto = new()
        {
            ClientId = Guid.NewGuid(),
            Status = "Pending",
            RouteId = Guid.NewGuid(),
            DeliveryPointId = Guid.NewGuid(),
            ClientOrderProducts = new List<OrderProduct>()
        };

        // Execute actual operation
        Result resultWithErrors = invalidDto.Validate();
        Result resultWithoutErrors = validDto.Validate();

        // Verify actual result
        Assert.False(resultWithErrors.IsSuccess);
        Assert.Equal(expectedErrorsQuantity, resultWithErrors.Errors.Count);

        Assert.True(resultWithoutErrors.IsSuccess);
    }
}
