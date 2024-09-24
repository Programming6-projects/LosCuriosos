namespace DistributionCenter.Infraestructure.Tests.DTOs.Concretes.Orders;

using DistributionCenter.Commons.Results;
using Domain.Entities.Concretes;
using Infraestructure.DTOs.Concretes.Orders;

public class CreateOrderDtoTests
{
    [Fact]
    public void ToEntity_ReturnsCorrectOrder()
    {
        // Define Input and Output
        CreateOrderDto dto = new()
        {
            ClientId = Guid.NewGuid(),
            DeliveryPointId = Guid.NewGuid(),
            OrderProducts = new List<OrderProductRequestDto>(),
        };

        // Execute actual operation
        Order client = dto.ToEntity();

        // Verify actual result
        Assert.Equal(dto.ClientId, client.ClientId);
        Assert.Equal(dto.DeliveryPointId, client.DeliveryPointId);
        Assert.Equal(dto.OrderProducts.Count, client.Products.Count);
    }

    [Fact]
    public void VerifyThatTheDataWasValidatedSuccessfully()
    {
        // Define Input and Output
        int expectedErrorsQuantity = 2;
        CreateOrderDto invalidDto = new()
        {
            ClientId = default,
            DeliveryPointId = default,
            OrderProducts = new List<OrderProductRequestDto>(),
        };

        CreateOrderDto validDto = new()
        {
            ClientId = Guid.NewGuid(),
            DeliveryPointId = Guid.NewGuid(),
            OrderProducts = new List<OrderProductRequestDto>(),
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
