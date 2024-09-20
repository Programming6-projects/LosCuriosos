namespace DistributionCenter.Infraestructure.Tests.DTOs.Concretes.Transports;

using Commons.Results;
using Domain.Entities.Concretes;
using Infraestructure.DTOs.Concretes.Transports;

public class UpdateTransportDtoTests
{
    [Fact]
    public void FromEntity_UpdatesTransportWithNewValues()
    {
        // Define Input and Output
        Transport existingTransport = new()
        {
            Name = "Old Truck",
            Plate = "1234ABC",
            Capacity = 2000000,
            CurrentCapacity = 1000000,
            IsAvailable = true
        };

        UpdateTransportDto dto = new()
        {
            Name = "Updated Truck",
            Capacity = 2500000,
            CurrentCapacity = 1800000,
            IsAvailable = false
        };

        // Execute actual operation
        Transport updatedTransport = dto.FromEntity(existingTransport);

        // Verify actual result
        Assert.Equal(dto.Name, updatedTransport.Name); Assert.Equal(dto.Capacity, updatedTransport.Capacity);
        Assert.Equal(dto.CurrentCapacity, updatedTransport.CurrentCapacity);
        Assert.Equal(dto.IsAvailable, updatedTransport.IsAvailable);
    }

    [Fact]
    public void FromEntity_DoesNotUpdateWhenDtoValuesAreNull()
    {
        // Define Input and Output
        Transport existingTransport = new()
        {
            Name = "Old Truck",
            Plate = "1234ABC",
            Capacity = 2000000,
            CurrentCapacity = 1000000,
            IsAvailable = true
        };

        UpdateTransportDto dto = new()
        {
            Name = null,
            Capacity = null,
            CurrentCapacity = null,
            IsAvailable = null
        };

        // Execute actual operation
        Transport updatedTransport = dto.FromEntity(existingTransport);

        // Verify actual result
        Assert.Equal("Old Truck", updatedTransport.Name);
        Assert.Equal("1234ABC", updatedTransport.Plate);
        Assert.Equal(2000000, updatedTransport.Capacity);
        Assert.Equal(1000000, updatedTransport.CurrentCapacity);
        Assert.True(updatedTransport.IsAvailable);
    }

    [Fact]
    public void Validate_ReturnsErrors_WhenDataIsInvalid()
    {
        // Define Input and Output
        int expectedErrorsCount = 2;
        UpdateTransportDto invalidDto = new()
        {
            Name = "T",
            Capacity = 50000,
            CurrentCapacity = 600000000,
            IsAvailable = true
        };

        UpdateTransportDto validDto = new()
        {
            Name = "Valid Truck",
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
