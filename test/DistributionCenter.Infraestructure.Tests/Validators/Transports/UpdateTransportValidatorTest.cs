namespace DistributionCenter.Infraestructure.Tests.Validators.Transports;

using Infraestructure.DTOs.Concretes.Transports;
using Infraestructure.Validators.Core.Concretes.Transports;

public class UpdateTransportValidatorTest
{
    [Fact]
    public void VerifyThanTransportNameIsValid()
    {
        // Define Input and Output
        UpdateTransportValidator validator = new();
        string invalidName = "";
        string validName = "Truck 001";

        // Execute actual operation
        UpdateTransportDto invalidDto = new()
        {
            Name = invalidName,
            Plate = "1234ABC",
            Capacity = 2000000,
            CurrentCapacity = 1500000,
            IsAvailable = true
        };
        UpdateTransportDto validDto = new()
        {
            Name = validName,
            Plate = "1234ABC",
            Capacity = 2000000,
            CurrentCapacity = 1500000,
            IsAvailable = true
        };

        // Verify actual result
        Assert.False(validator.Validate(invalidDto).IsSuccess);
        Assert.True(validator.Validate(validDto).IsSuccess);
    }

    [Fact]
    public void VerifyThanTransportPlateIsValid()
    {
        // Define Input and Output
        UpdateTransportValidator validator = new();
        string invalidPlate = "A12ABC";
        string validPlate = "1234ABC";

        // Execute actual operation
        UpdateTransportDto invalidDto = new()
        {
            Name = "Truck 001",
            Plate = invalidPlate,
            Capacity = 2000000,
            CurrentCapacity = 1500000,
            IsAvailable = true
        };
        UpdateTransportDto validDto = new()
        {
            Name = "Truck 002",
            Plate = validPlate,
            Capacity = 2000000,
            CurrentCapacity = 1500000,
            IsAvailable = true
        };

        // Verify actual result
        Assert.False(validator.Validate(invalidDto).IsSuccess);
        Assert.True(validator.Validate(validDto).IsSuccess);
    }

    [Fact]
    public void VerifyThanTransportCapacityIsWithinRange()
    {
        // Define Input and Output
        UpdateTransportValidator validator = new();
        int invalidCapacity = 499000;
        int validCapacity = 2000000;

        // Execute actual operation
        UpdateTransportDto invalidDto = new()
        {
            Name = "Truck 003",
            Plate = "1234ABC",
            Capacity = invalidCapacity,
            CurrentCapacity = 1500000,
            IsAvailable = true
        };
        UpdateTransportDto validDto = new()
        {
            Name = "Truck 004",
            Plate = "1234ABC",
            Capacity = validCapacity,
            CurrentCapacity = 1500000,
            IsAvailable = true
        };

        // Verify actual result
        Assert.False(validator.Validate(invalidDto).IsSuccess);
        Assert.True(validator.Validate(validDto).IsSuccess);
    }

    [Fact]
    public void VerifyThanTransportCurrentCapacityIsWithinRange()
    {
        // Define Input and Output
        UpdateTransportValidator validator = new();
        int invalidCurrentCapacity = 460000000;
        int validCurrentCapacity = 3000000;

        // Execute actual operation
        UpdateTransportDto invalidDto = new()
        {
            Name = "Truck 005",
            Plate = "1234ABC",
            Capacity = 2000000,
            CurrentCapacity = invalidCurrentCapacity,
            IsAvailable = true
        };
        UpdateTransportDto validDto = new()
        {
            Name = "Truck 006",
            Plate = "1234ABC",
            Capacity = 2000000,
            CurrentCapacity = validCurrentCapacity,
            IsAvailable = true
        };

        // Verify actual result
        Assert.False(validator.Validate(invalidDto).IsSuccess);
        Assert.True(validator.Validate(validDto).IsSuccess);
    }

    [Fact]
    public void VerifyThanTransportIsAvailableWhenNotNull()
    {
        // Define Input and Output
        UpdateTransportValidator validator = new();
        bool validIsAvailable = true;

        // Execute actual operation
        UpdateTransportDto validDto = new()
        {
            Name = "Truck 008",
            Plate = "1234ABC",
            Capacity = 2000000,
            CurrentCapacity = 1500000,
            IsAvailable = validIsAvailable
        };

        // Verify actual result
        Assert.True(validator.Validate(validDto).IsSuccess);
    }
}
