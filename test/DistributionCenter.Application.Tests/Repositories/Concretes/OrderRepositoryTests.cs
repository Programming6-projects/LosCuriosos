namespace DistributionCenter.Application.Tests.Repositories.Concretes;

using Application.Contexts.Interfaces;
using Application.Repositories.Concretes;

public class OrderRepositoryTests
{
    [Fact]
    public void Constructor_InitializesRepository()
    {
        // Define Input and Output
        Mock<IContext> contextMock = new();
        OrderRepository repository = new(contextMock.Object);

        // Verify actual result
        Assert.NotNull(repository);
    }
}
