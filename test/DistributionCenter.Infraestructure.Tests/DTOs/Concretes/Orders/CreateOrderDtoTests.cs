namespace DistributionCenter.Infraestructure.Tests.DTOs.Concretes.Orders;

using Commons.Results;
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
                ClientId = new Guid(),
                OrderStatusId = new Guid(),
            };

        // Execute actual operation
        Order client = dto.ToEntity();

        // Verify actual result
        Assert.Equal(dto.ClientId, client.ClientId);
        Assert.Equal(dto.OrderStatusId, client.OrderStatusId);
    }

    [Fact]
    public void VerifyThatTheDataWasValidatedSuccessfully()
    {
        // Define Input and Output
        int expectedErrorsQuantity = 2;
        CreateOrderDto invalidDto =
            new()
            {
                ClientId = default,
                OrderStatusId = default,
            };

        CreateOrderDto validDto =
            new()
            {
                ClientId = new Guid("a2b6e412-4b7e-45c9-a78e-58f0e4e54b2d"),
                OrderStatusId = new Guid("7683ba92-0f00-4000-b86c-f51273ce34b8"),
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
