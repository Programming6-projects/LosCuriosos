namespace DistributionCenter.Api.Tests.Controllers;

public class RemindersControllerTests(WebApplicationFactory<Startup> factory)
    : IClassFixture<WebApplicationFactory<Startup>>
{
    private readonly WebApplicationFactory<Startup> _factory = factory;

    [Fact]
    public async Task Get_ReturnsOkResult()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.GetAsync(new Uri("/api/reminders", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_ReturnsCorrectMessage()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();

        // Act
        string response = await client.GetStringAsync(new Uri("/api/reminders", UriKind.Relative));

        // Assert
        Assert.Equal("Hello from RemindersController", response);
    }
}