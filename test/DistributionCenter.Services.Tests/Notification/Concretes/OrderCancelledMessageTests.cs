namespace DistributionCenter.Services.Tests.Notification.Concretes;

public class OrderCancelledMessageTests
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
                        <h1>Order Cancelled</h1>
                    </div>
                    <div class='content'>
                        <p>We regret to inform you that your order with ID <strong>{orderId}</strong> has been cancelled.</p>
                        <p>If you have any questions, please contact our support team.</p>
                    </div>
                    <div class='footer'>
                        <p>We apologize for any inconvenience caused.</p>
                    </div>
                </div>
            </body>
            </html>";
        OrderCancelledMessage message = new(orderId);

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
        OrderCancelledMessage message = new(orderId);

        // Act
        string result = message.Subject;

        // Assert
        Assert.Equal("Order Processing Error", result);
    }
}
