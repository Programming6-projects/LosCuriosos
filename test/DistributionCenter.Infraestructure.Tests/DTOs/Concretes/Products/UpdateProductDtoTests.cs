namespace DistributionCenter.Infraestructure.Tests.DTOs.Concretes.Products;

using Commons.Results;
using Domain.Entities.Concretes;
using Infraestructure.DTOs.Concretes.Products;

public class UpdateProductDtoTests
{
    [Fact]
    public void FromEntity_UpdatesAndReturnsCorrectProduct()
    {
        // Define Input and Output
        double expectedWeight = 100f;
        Product product =
            new()
            {
                Name = "Pepsi Zero Personal",
                Description = "Lorem ipsum dolor bae",
                Weight = expectedWeight,
            };
        UpdateProductDto dto =
            new()
            {
                Name = "Pepsi Zero Personal",
                Description = "Lorem ipsum dolor bae"
            };

        // Execute actual operation
        Product updatedProduct = dto.FromEntity(product);

        // Verify actual result
        Assert.Equal(dto.Name, updatedProduct.Name);
        Assert.Equal(dto.Description, updatedProduct.Description);
        Assert.Equal(expectedWeight, updatedProduct.Weight);
    }

    [Fact]
    public void FromEntity_UpdatesWithNullsAndReturnsCorrectProduct()
    {
        // Define Input and Output
        double expectedWeight = 100f;
        Product product =
            new()
            {
                Name = "Pepsi Zero Personal",
                Description = "Lorem ipsum dolor bae",
                Weight = expectedWeight,
            };
        UpdateProductDto dto = new()
        {
            Name = null,
            Description = null
        };

        // Execute actual operation
        Product updatedProduct = dto.FromEntity(product);

        // Verify actual result
        Assert.Equal(product.Name, updatedProduct.Name);
        Assert.Equal(product.Description, updatedProduct.Description);
        Assert.Equal(product.Weight, updatedProduct.Weight);
    }

    [Fact]
    public void VerifyThatTheDataWasValidatedSuccessfully()
    {
        // Define Input and Output
        int expectedErrorsQuantity = 2;
        UpdateProductDto invalidDto =
            new()
            {
                Name = "Sh",
                Description = "or"
            };

        UpdateProductDto validDto =
            new()
            {
                Name = "Valid Name 2",
                Description = "Valid Description"
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
