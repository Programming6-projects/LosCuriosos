namespace DistributionCenter.Services.Tests.Notification.Concretes;

using DistributionCenter.Services.Notification.Concretes;

public class OrderShippedMessageTests
{
    [Fact]
    public void GetMessage_ReturnsExpectedMessage()
    {
        // Arrange
        Guid orderId = Guid.NewGuid();
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
                        <h1>Transport Assigned</h1>
                    </div>
                    <div class='content'>
                        <p>A transport has been assigned to your order with ID <strong>{orderId}</strong>.</p>
                        <p>Your order is now on its way!</p>
                    </div>
                    <div class='footer'>
                        <p>Thank you for your patience.</p>
                    </div>
                </div>
            </body>
            </html>";
        OrderShippedMessage message = new(orderId);

        // Act
        string result = message.GetMessage();

        // Assert
        Assert.Equal(expectedMessage, result);
    }

    [Fact]
    public void Subject_ReturnsExpectedSubject()
    {
        // Arrange
        Guid orderId = Guid.NewGuid();
        OrderShippedMessage message = new(orderId);

        // Act
        string result = message.Subject;

        // Assert
        Assert.Equal("Transport Assigned to Your Order", result);
    }
}
