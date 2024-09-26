namespace DistributionCenter.Infraestructure.Tests.DTOs.Concretes.Orders;

using DistributionCenter.Commons.Results;
using Domain.Entities.Concretes;
using Infraestructure.DTOs.Concretes.Orders;

public class CreateOrderDtoTests
{
    [Fact]
    public void ToEntity_ReturnsCorrectOrder()
    {
        Guid clientId = Guid.NewGuid();
        Guid deliveryPointId = Guid.NewGuid();
        CreateOrderDto dto = new ()
        {
            ClientId = clientId,
            DeliveryPointId = deliveryPointId,
            OrderProducts = new List<OrderProductRequestDto>(),
        };

        Order order = dto.ToEntity();

        // Verify actual result
        Assert.Equal(dto.ClientId, order.ClientId);
        Assert.Equal(dto.DeliveryPointId, order.DeliveryPointId);
        Assert.Empty(order.Products);
    }

    [Fact]
    public void VerifyThatTheDataWasValidatedSuccessfully()
    {
        // Define Input and Output
        CreateOrderDto invalidDto = new ()
        {
            ClientId = Guid.Empty,
            DeliveryPointId = Guid.Empty,
            OrderProducts = new List<OrderProductRequestDto>
            {
                new() {
                    ProductId = Guid.NewGuid(),
                    Quantity = -1
                }
            }
        };

        CreateOrderDto validDto = new()
        {
            ClientId = Guid.NewGuid(),
            DeliveryPointId = Guid.NewGuid(),
            OrderProducts = new List<OrderProductRequestDto>
            {
                new() {
                    ProductId = Guid.NewGuid(),
                    Quantity = 1
                }
            }
        };

        Result resultWithErrors = invalidDto.Validate();
        Result resultWithoutErrors = validDto.Validate();

        Assert.False(resultWithErrors.IsSuccess);
        _ = Assert.Single(resultWithErrors.Errors);

        Assert.True(resultWithoutErrors.IsSuccess);
    }

    [Fact]
    public void Validate_WhenDtoHasNullProperties_ReturnsErrors()
    {
        CreateOrderDto dto = new()
        {
            ClientId = Guid.Empty,
            DeliveryPointId = Guid.Empty,
            OrderProducts = new List<OrderProductRequestDto>
            {
                new() {
                    ProductId = Guid.NewGuid(),
                    Quantity = -1
                }
            }
        };

        Result result = dto.Validate();

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public void Validate_WhenDtoIsValid_ReturnsSuccess()
    {
        CreateOrderDto dto = new()
        {
            ClientId = Guid.NewGuid(),
            DeliveryPointId = Guid.NewGuid(),
            OrderProducts = new List<OrderProductRequestDto>
            {
                new() {
                    ProductId = Guid.NewGuid(),
                    Quantity = 1
                }
            }
        };

        Result result = dto.Validate();

        // Assert
        Assert.True(result.IsSuccess);

        if (!result.IsSuccess)
        {
            Assert.Empty(result.Errors);
        }
    }
}
