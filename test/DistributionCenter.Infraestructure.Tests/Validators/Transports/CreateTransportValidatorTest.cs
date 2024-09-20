namespace DistributionCenter.Infraestructure.Tests.Validators.Transports;

public class CreateTransportValidatorTest
{
    [Fact]
    public void VerifyThanTransportNameIsValid()
    {
        // Define Input and Output
        CreateTransportValidator validator = new();
        string invalidName = "";
        string validName = "Truck 001";

        // Execute actual operation
        CreateTransportDto invalidDto = new()
        {
            Name = invalidName,
            Plate = "1234ABC",
            Capacity = 2000000,
            CurrentCapacity = 1500000,
            IsAvailable = true
        };
        CreateTransportDto validDto = new()
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
        CreateTransportValidator validator = new();
        string invalidPlate = "A12ABC";
        string validPlate = "1234ABC";

        // Execute actual operation
        CreateTransportDto invalidDto = new()
        {
            Name = "Truck 001",
            Plate = invalidPlate,
            Capacity = 2000000,
            CurrentCapacity = 1500000,
            IsAvailable = true
        };
        CreateTransportDto validDto = new()
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
        CreateTransportValidator validator = new();
        int invalidCapacity = 499000;
        int validCapacity = 2000000;

        // Execute actual operation
        CreateTransportDto invalidDto = new()
        {
            Name = "Truck 003",
            Plate = "1234ABC",
            Capacity = invalidCapacity,
            CurrentCapacity = 1500000,
            IsAvailable = true
        };
        CreateTransportDto validDto = new()
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
        CreateTransportValidator validator = new();
        int invalidCurrentCapacity = 460000000;
        int validCurrentCapacity = 3000000;

        // Execute actual operation
        CreateTransportDto invalidDto = new()
        {
            Name = "Truck 005",
            Plate = "1234ABC",
            Capacity = 2000000,
            CurrentCapacity = invalidCurrentCapacity,
            IsAvailable = true
        };
        CreateTransportDto validDto = new()
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
    public void VerifyThanTransportCapacityIsNonNegative()
    {
        // Define Input and Output
        CreateTransportValidator validator = new();
        int invalidCapacity = -1000;
        int validCapacity = 1000000;

        // Execute actual operation
        CreateTransportDto invalidDto = new()
        {
            Name = "Truck 007",
            Plate = "1234ABC",
            Capacity = invalidCapacity,
            CurrentCapacity = 1500000,
            IsAvailable = true
        };
        CreateTransportDto validDto = new()
        {
            Name = "Truck 008",
            Plate = "1234ABC",
            Capacity = validCapacity,
            CurrentCapacity = 1500000,
            IsAvailable = true
        };

        // Verify actual result
        Assert.False(validator.Validate(invalidDto).IsSuccess);
        Assert.True(validator.Validate(validDto).IsSuccess);
    }
}

