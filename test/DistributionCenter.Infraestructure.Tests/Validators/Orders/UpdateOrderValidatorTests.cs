namespace DistributionCenter.Infraestructure.Tests.Validators.Orders;

using DistributionCenter.Infraestructure.Validators.Core.Concretes.Orders;

public class UpdateOrderValidatorTests
{
    [Fact]
    public void Constructor_InitializesSuccessfully()
    {
        // Act
        UpdateOrderValidator validator = new();

        // Assert
        Assert.NotNull(validator);
    }
}
