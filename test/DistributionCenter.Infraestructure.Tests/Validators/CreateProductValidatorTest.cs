namespace DistributionCenter.Infraestructure.Tests.Validators;

using Infraestructure.DTOs.Concretes.Products;
using Infraestructure.Validators.Core.Concretes.Products;

public class CreateProductValidatorTest
{
    [Fact]
    public void VerifyThanProductNameHasAtLeast3Characters()
    {
        // Define Input and Output
        CreateProductValidator validator = new();
        string invalidName = "a";
        string validName = "Peps";

        // Execute actual operation
        CreateProductDto invalidDto = new()
        {
            Name = invalidName,
            Description = "Some long and valid description",
            Weight = 1234
        };
        CreateProductDto validDto = new()
        {
            Name = validName,
            Description = "Some long and valid description",
            Weight = 1234
        };

        // Verify actual result
        Assert.False(validator.Validate(invalidDto).IsSuccess);
        Assert.True(validator.Validate(validDto).IsSuccess);
    }

    [Fact]
    public void VerifyThanProductNameHasLeastThan64Characters()
    {
        // Define Input and Output
        CreateProductValidator validator = new();
        string invalidName = "Pepsi beats Coca Cola because Coca Cola has a lot of sugar." +
                             " And Pepsi always has better announcements than Coca-Cola and other soft drinks.";
        string validName = "Pepsi Zero 2Lts";

        // Execute actual operation
        CreateProductDto invalidDto = new()
        {
            Name = invalidName,
            Description = "Some long and valid description",
            Weight = 10
        };
        CreateProductDto validDto = new()
        {
            Name = validName,
            Description = "Some long and valid description",
            Weight = 1867
        };

        // Verify actual result
        Assert.False(validator.Validate(invalidDto).IsSuccess);
        Assert.True(validator.Validate(validDto).IsSuccess);
    }

    [Fact]
    public void VerifyThanProductDescriptionHasAtLeast3Characters()
    {
        // Define Input and Output
        CreateProductValidator validator = new();
        string invalidDescription = "a";
        string validDescription = "This is a description Valid 1";

        // Execute actual operation
        CreateProductDto invalidDto = new()
        {
            Name = "Pepsi Zero 2Lts",
            Description = invalidDescription,
            Weight = 1000
        };
        CreateProductDto validDto = new()
        {
            Name = "Pepsi Zero 2Lts",
            Description = validDescription,
            Weight = 1000
        };

        // Verify actual result
        Assert.False(validator.Validate(invalidDto).IsSuccess);
        Assert.True(validator.Validate(validDto).IsSuccess);
    }

    [Fact]
    public void VerifyThanProductDescriptionHasLeastThan128Characters()
    {
        // Define Input and Output
        CreateProductValidator validator = new();
        string invalidDescription = "Lorem ipsum dolor sit amet consectetur adipiscing elit Ut et massa mi." +
                                    " Aliquam in hendrerit urna. Pellentesque sit amet sapien fringilla, mattis " +
                                    "ligula consectetur,.";
        string validDescription = "Lorem ipsum dolor sit amet consectetur adipiscing elit Ut et massa mi." +
                                  " Aliquam in hendrerit urna amet sapien fringilla, mattis l";

        // Execute actual operation
        CreateProductDto invalidDto = new()
        {
            Name = "Pepsi Zero 2Lts",
            Description = invalidDescription,
            Weight = 1000
        };
        CreateProductDto validDto = new()
        {
            Name = "Pepsi Zero 2Lts",
            Description = validDescription,
            Weight = 1000
        };

        // Verify actual result
        Assert.False(validator.Validate(invalidDto).IsSuccess);
        Assert.True(validator.Validate(validDto).IsSuccess);
    }

    [Fact]
    public void VerifyThanProductWeightHasALimitDecimalNumbers()
    {
        // Define Input and Output
        CreateProductValidator validator = new();
        uint invalidWeight = 1492850;
        uint validWeight = 149285;

        // Execute actual operation
        CreateProductDto invalidDto = new()
        {
            Name = "Pepsi Zero 2Lts",
            Description = "This is a good description",
            Weight = invalidWeight
        };
        CreateProductDto validDto = new()
        {
            Name = "Pepsi Zero 2Lts",
            Description = "This is a good description",
            Weight = validWeight
        };

        // Verify actual result
        Assert.False(validator.Validate(invalidDto).IsSuccess);
        Assert.True(validator.Validate(validDto).IsSuccess);
    }
}
