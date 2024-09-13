namespace DistributionCenter.Services.Tests.Notification.Concretes;

using DistributionCenter.Services.Notification.Concretes;

public class ErrorNotificationMessageTests
{
    [Fact]
    public void GetMessage_ReturnsExpectedMessage()
    {
        // Arrange
        string errorMessage = "Test error message";
        string expectedMessage =
            $@"
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; margin: 20px; }}
                        .container {{ max-width: 600px; margin: auto; }}
                        .header {{ background: #f8f8f8; padding: 10px; text-align: center; }}
                        .content {{ margin: 20px 0; }}
                        .footer {{ background: #f8f8f8; padding: 10px; text-align: center; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h1>Order Processing Error</h1>
                        </div>
                        <div class='content'>
                            <p>We encountered an error while processing your order.</p>
                            <p><strong>Error:</strong> {errorMessage}</p>
                            <p>Please follow these steps to try again:</p>
                            <ul>
                                <li>Check your order details and try again.</li>
                                <li>If the problem persists, contact our support team at loscuriosos@gmail.com.</li>
                            </ul>
                        </div>
                        <div class='footer'>
                            <p>Thank you for your patience!</p>
                        </div>
                    </div>
                </body>
                </html>";
        ErrorNotificationMessage message = new(errorMessage);

        // Act
        string result = message.GetMessage();

        // Assert
        Assert.Equal(expectedMessage, result);
    }

    [Fact]
    public void Subject_ReturnsExpectedSubject()
    {
        // Arrange
        string errorMessage = "Test error message";
        ErrorNotificationMessage message = new(errorMessage);

        // Act
        string result = message.Subject;

        // Assert
        Assert.Equal("Order Processing Error", result);
    }
}
