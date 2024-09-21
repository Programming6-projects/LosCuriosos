namespace DistributionCenter.Application.Tests.Repositories.Concretes;

using Application.Contexts.Interfaces;

public class TripRepositoryTest
{
    [Fact]
    public void Constructor_InitializesRepository()
    {
        // Define Input and Output
        Mock<IContext> contextMock = new();
        TripRepository repository = new(contextMock.Object);

        // Verify actual result
        Assert.NotNull(repository);
    }
}
