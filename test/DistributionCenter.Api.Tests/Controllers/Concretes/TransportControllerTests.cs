namespace DistributionCenter.Api.Tests.Controllers.Concretes;

using Application.Repositories.Interfaces;
using Domain.Entities.Concretes;

public class TransportControllerTests
{
    private readonly Mock<IRepository<Transport>> _repositoryMock;

    public TransportControllerTests()
    {
        _repositoryMock = new Mock<IRepository<Transport>>();
    }

    [Fact]
    public void Constructor_InitializesController()
    {
        // Define Input and Output
        TransportController controller = new(_repositoryMock.Object);

        // Verify actual result
        Assert.NotNull(controller);
    }
}
