namespace DistributionCenter.Infraestructure.Tests.DTOs.Concretes.Transports;

using Commons.Results;
using Domain.Entities.Concretes;
using Infraestructure.DTOs.Concretes.Transports;

public class CreateTransportDtoTests
{
    [Fact]
    public void ToEntity_ReturnsCorrectTransport()
    {
        // Define Input and Output
        CreateTransportDto dto = new()
        {
            Name = "Truck A",
            Plate = "1234ABC",
            Capacity = 2000000,
            CurrentCapacity = 1500000,
            IsAvailable = true
        };

        // Execute actual operation
        Transport transport = dto.ToEntity();

        // Verify actual result
        Assert.Equal(dto.Name, transport.Name);
        Assert.Equal(dto.Plate, transport.Plate);
        Assert.Equal(dto.Capacity, transport.Capacity);
        Assert.Equal(dto.CurrentCapacity, transport.CurrentCapacity);
        Assert.Equal(dto.IsAvailable, transport.IsAvailable);
    }

    [Fact]
    public void Validate_ReturnsErrors_WhenDataIsInvalid()
    {
        // Define Input and Output
        int expectedErrorsCount = 3;
        CreateTransportDto invalidDto = new()
        {
            Name = "T",
            Plate = "1234A",
            Capacity = 50000,
            CurrentCapacity = 600000000,
            IsAvailable = true
        };

        CreateTransportDto validDto = new()
        {
            Name = "Valid Truck",
            Plate = "1234ABC",
            Capacity = 2000000,
            CurrentCapacity = 1500000,
            IsAvailable = true
        };

        // Execute actual operation
        Result resultWithErrors = invalidDto.Validate();
        Result resultWithoutErrors = validDto.Validate();

        // Verify actual result
        Assert.False(resultWithErrors.IsSuccess);
        Assert.Equal(expectedErrorsCount, resultWithErrors.Errors.Count);

        Assert.True(resultWithoutErrors.IsSuccess);
    }
}
