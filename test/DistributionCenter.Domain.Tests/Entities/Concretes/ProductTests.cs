namespace DistributionCenter.Domain.Tests.Entities.Concretes;

using Domain.Entities.Concretes;

public class ProductTests
{
    [Fact]
    public void CreateProductTest()
    {
        // Define Input and output
        string expectedName = "Pepsico Zero 2Lts.";
        string expectedDescription = "Fresh analcoholic beverage without sugar.";
        double expectedWeight = 10.53;

        // Execute actual operation
        Product product = new()
        {
            Name = expectedName,
            Description = expectedDescription,
            Weight = expectedWeight
        };

        // Verify actual result
        Assert.Equal(expectedName, product.Name);
        Assert.Equal(expectedDescription, product.Description);
        Assert.Equal(expectedWeight, product.Weight);
    }

    [Fact]
    public void EditProductTest()
    {
        // Define Input and output
        string previousName = "Pepsico Zero 2Lts.";
        string editedName = "Pepsico Zero 3Lts.";
        string previousDescription = "Fresh analcoholic beverage without sugar.";
        string editedDescription = "Fresh analcoholic beverage without sugar for diabetics.";
        double previousWeight = 10.53;
        double editedWeight = 200f;

        Product product = new()
        {
            Name = previousName,
            Description = previousDescription,
            Weight = previousWeight
        };

        // Execute actual operation
        product.Name = editedName;
        product.Description = editedDescription;
        product.Weight = editedWeight;

        // Verify actual result
        Assert.Equal(editedName, product.Name);
        Assert.Equal(editedDescription, product.Description);
        Assert.Equal(editedWeight, product.Weight);
    }
}
