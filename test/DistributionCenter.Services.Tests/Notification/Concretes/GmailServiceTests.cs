namespace DistributionCenter.Services.Tests.Notification.Concretes;

public class GmailServiceTests
{
    [Fact]
    public void Should_Instance_Not_Be_Null()
    {
        // Define Input and Ouput
        GmailService service = new("username", "password");

        // Verify actual result
        Assert.NotNull(service);
    }
}
