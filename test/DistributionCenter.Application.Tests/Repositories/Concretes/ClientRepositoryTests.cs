namespace DistributionCenter.Application.Tests.Repositories.Concretes;

using DistributionCenter.Application.Contexts.Interfaces;

public class ClientRepositoryTests
{
    [Fact]
    public void Constructor_InitializesRepository()
    {
        // Define Input and Output
        Mock<IContext> contextMock = new();
        ClientRepository repository = new(contextMock.Object);

        // Verify actual result
        Assert.NotNull(repository);
    }
}
