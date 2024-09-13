namespace DistributionCenter.Application.Tests.Repositories.Concretes;

using DistributionCenter.Application.Contexts.Interfaces;

public class ProductRepositoryTests
{
    [Fact]
    public void Constructor_InitializesRepository()
    {
        // Define Input and Output
        Mock<IContext> contextMock = new();
        ProductRepository repository = new(contextMock.Object);

        // Verify actual result
        Assert.NotNull(repository);
    }
}
