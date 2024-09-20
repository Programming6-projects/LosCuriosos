namespace DistributionCenter.Api.Tests.Controllers.Concretes;

using Application.Repositories.Interfaces;
using Domain.Entities.Concretes;

public class TripControllerTests
{
    private readonly Mock<IRepository<Trip>> _repositoryMock;

    public TripControllerTests()
    {
        _repositoryMock = new Mock<IRepository<Trip>>();
    }

    [Fact]
    public void Constructor_InitializesController()
    {
        // Define Input and Output
        TripController controller = new(_repositoryMock.Object);

        // Verify actual result
        Assert.NotNull(controller);
    }
}
