namespace DistributionCenter.Services.Tests.NotificationService.Concretes;

using DistributionCenter.Services.NotificationService.Concretes;
using Xunit;

public class HtmlMessageTests
{
    [Fact]
    public void GetMessageContent_ReturnsCorrectContent()
    {
        // Arrange
        string content = "<html><body>Test message</body></html>";
        HtmlMessage message = new(content);

        // Act
        string result = message.GetMessageContent();

        // Assert
        Assert.Equal(content, result);
    }

    [Fact]
    public void CreateOrderConfirmation_ReturnsSuccessOrderConfirmation()
    {
        // Arrange
        string orderId = "123456";

        // Act
        HtmlMessage message = HtmlMessage.CreateOrderConfirmation(orderId);
        string result = message.GetMessageContent();

        // Assert
        Assert.True(result.Contains("<h1>Order Confirmation</h1>", StringComparison.Ordinal));
        Assert.True(result.Contains($"<strong>{orderId}</strong>", StringComparison.Ordinal));
    }

    [Fact]
    public void CreateErrorNotification_ReturnsErrorNotification()
    {
        // Arrange
        string errorMessage = "There was an issue processing your request. Please try again.";

        // Act
        HtmlMessage message = HtmlMessage.CreateErrorNotification(errorMessage);
        string result = message.GetMessageContent();

        // Assert
        Assert.True(result.Contains("<h1>Order Processing Error</h1>", StringComparison.Ordinal));
        Assert.True(result.Contains($"<strong>Error:</strong> {errorMessage}", StringComparison.Ordinal));
        Assert.True(result.Contains("<p>Please follow these steps to try again:</p>", StringComparison.Ordinal));
        Assert.True(result.Contains("<ul>", StringComparison.Ordinal));
        Assert.True(result.Contains("<li>Check your order details and try again.</li>", StringComparison.Ordinal));
        Assert.True(result.Contains("<li>If the problem persists, contact our support team at loscuriosos@gmail.com.</li>", StringComparison.Ordinal));
        Assert.True(result.Contains("</ul>", StringComparison.Ordinal));
    }

    [Fact]
    public void CreateOrderConfirmation_ContainsOrderId()
    {
        // Arrange
        string orderId = "7890";
        HtmlMessage message = HtmlMessage.CreateOrderConfirmation(orderId);

        // Act
        string content = message.GetMessageContent();

        // Assert
        Assert.Contains(orderId, content, StringComparison.OrdinalIgnoreCase); // Specify StringComparison
    }

    [Fact]
    public void CreateErrorNotification_ContainsErrorMessage()
    {
        // Arrange
        string errorMessage = "Sample error";
        HtmlMessage message = HtmlMessage.CreateErrorNotification(errorMessage);

        // Act
        string content = message.GetMessageContent();

        // Assert
        Assert.Contains(errorMessage, content, StringComparison.OrdinalIgnoreCase); // Specify StringComparison
    }
}
