namespace DistributionCenter.Application.Tests.Contexts.Concretes;

using DistributionCenter.Application.Contexts.Concretes;

public class ContextTests
{
    [Fact]
    public void Constructor_InitializesController()
    {
        // Define Input and Output
        Context controller = new(new Dictionary<Type, object>());

        // Verify actual result
        Assert.NotNull(controller);
    }
}
