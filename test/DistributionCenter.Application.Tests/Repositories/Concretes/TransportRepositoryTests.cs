namespace DistributionCenter.Application.Tests.Repositories.Concretes;

using Application.Contexts.Interfaces;

public class TransportRepositoryTests
{
    [Fact]
    public void Constructor_InitializesRepository()
    {
        // Define Input and Output
        Mock<IContext> contextMock = new();
        TransportRepository repository = new(contextMock.Object);

        // Verify actual result
        Assert.NotNull(repository);
    }
}
