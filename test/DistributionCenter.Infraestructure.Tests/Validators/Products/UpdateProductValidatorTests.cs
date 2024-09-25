namespace DistributionCenter.Infraestructure.Tests.Validators.Products;

using DistributionCenter.Infraestructure.DTOs.Concretes.Products;
using DistributionCenter.Infraestructure.Validators.Core.Concretes.Products;

public class UpdateProductValidatorTests
{
    [Fact]
    public void VerifyThanProductNameHasAtLeast3Characters()
    {
        // Define Input and Output
        UpdateProductValidator validator = new();
        string invalidName = "a";
        string validName = "Pepsi Zero 2Lts";

        // Execute actual operation
        UpdateProductDto invalidDto = new()
        {
            Name = invalidName,
            Description = "Some long and valid description",
            Weight = 1000
        };
        UpdateProductDto validDto = new()
        {
            Name = validName,
            Description = "Some long and valid description",
            Weight = 1000
        };

        // Verify actual result
        Assert.False(validator.Validate(invalidDto).IsSuccess);
        Assert.True(validator.Validate(validDto).IsSuccess);
    }

    [Fact]
    public void VerifyThanProductNameHasLeastThan64Characters()
    {
        // Define Input and Output
        UpdateProductValidator validator = new();
        string invalidName = "Pepsi beats Coca Cola because Coca Cola has a lot of sugar." +
                             " And Pepsi always has better announcements than Coca-Cola and other soft drinks.";
        string validName = "Pepsi Zero 2Lts";

        // Execute actual operation
        UpdateProductDto invalidDto = new()
        {
            Name = invalidName,
            Description = "Some long and valid description",
            Weight = 1000
        };
        UpdateProductDto validDto = new()
        {
            Name = validName,
            Description = "Some long and valid description",
            Weight = 1000
        };

        // Verify actual result
        Assert.False(validator.Validate(invalidDto).IsSuccess);
        Assert.True(validator.Validate(validDto).IsSuccess);
    }

    [Fact]
    public void VerifyThanProductDescriptionHasAtLeast3Characters()
    {
        // Define Input and Output
        UpdateProductValidator validator = new();
        string invalidDescription = "a";
        string validDescription = "This is a description Valid 1";

        // Execute actual operation
        UpdateProductDto invalidDto = new()
        {
            Name = "Pepsi Zero 2Lts",
            Description = invalidDescription,
            Weight = 1000
        };
        UpdateProductDto validDto = new()
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
        UpdateProductValidator validator = new();
        string invalidDescription = "Lorem ipsum dolor sit amet consectetur adipiscing elit Ut et massa mi." +
                                    " Aliquam in hendrerit urna. Pellentesque sit amet sapien fringilla, mattis " +
                                    "ligula consectetur,.";
        string validDescription = "Lorem ipsum dolor sit amet consectetur adipiscing elit Ut et massa mi." +
                                  " Aliquam in hendrerit urna amet sapien fringilla, mattis l";

        // Execute actual operation
        UpdateProductDto invalidDto = new()
        {
            Name = "Pepsi Zero 2Lts",
            Description = invalidDescription,
            Weight = 1000
        };
        UpdateProductDto validDto = new()
        {
            Name = "Pepsi Zero 2Lts",
            Description = validDescription,
            Weight = 1000
        };

        // Verify actual result
        Assert.False(validator.Validate(invalidDto).IsSuccess);
        Assert.True(validator.Validate(validDto).IsSuccess);
    }
}
