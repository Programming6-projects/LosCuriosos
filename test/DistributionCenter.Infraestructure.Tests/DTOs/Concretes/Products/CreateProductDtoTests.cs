namespace DistributionCenter.Infraestructure.Tests.DTOs.Concretes.Products;

using Commons.Results;
using Domain.Entities.Concretes;
using Infraestructure.DTOs.Concretes.Products;

public class CreateProductDtoTests
{
    [Fact]
    public void ToEntity_ReturnsCorrectClient()
    {
        // Define Input and Output
        CreateProductDto dto =
            new()
            {
                Name = "Pepsi Light Popular",
                Description = "Some long description",
                Weight = 1000f,
            };

        // Execute actual operation
        Product product = dto.ToEntity();

        // Verify actual result
        Assert.Equal(dto.Name, product.Name);
        Assert.Equal(dto.Description, product.Description);
        Assert.Equal(dto.Weight, product.Weight);
    }

    [Fact]
    public void VerifyThatTheDataWasValidatedSuccessfully()
    {
        // Define Input and Output
        int expectedErrorsQuantity = 3;
        CreateProductDto invalidDto =
            new()
            {
                Name = "Sh",
                Description = "or",
                Weight = 13.5352342,
            };

        CreateProductDto validDto =
            new()
            {
                Name = "Valid Name 2",
                Description = "Valid Description",
                Weight = 13.53,
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
