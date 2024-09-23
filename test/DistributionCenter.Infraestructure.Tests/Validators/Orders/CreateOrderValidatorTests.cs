namespace DistributionCenter.Infraestructure.Tests.Validators.Orders;

using DistributionCenter.Infraestructure.Validators.Core.Concretes.Orders;

public class CreateOrderValidatorTests
{
    [Fact]
    public void Constructor_InitializesSuccessfully()
    {
        // Act
        CreateOrderValidator validator = new();

        // Assert
        Assert.NotNull(validator);
    }
}
