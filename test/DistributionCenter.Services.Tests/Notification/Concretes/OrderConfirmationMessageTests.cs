namespace DistributionCenter.Services.Tests.Notification.Concretes;

using DistributionCenter.Services.Notification.Concretes;

public class OrderConfirmationMessageTests
{
    [Fact]
    public void GetMessage_ReturnsExpectedMessage()
    {
        // Arrange
        string orderId = "Test order ID";
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
                        <h1>Order Confirmation</h1>
                    </div>
                    <div class='content'>
                        <p>Your order with ID <strong>{orderId}</strong> has been received and is being processed.</p>
                    </div>
                    <div class='footer'>
                        <p>Thank you for choosing us!</p>
                    </div>
                </div>
            </body>
            </html>";
        OrderConfirmationMessage message = new(orderId);

        // Act
        string result = message.GetMessage();

        // Assert
        Assert.Equal(expectedMessage, result);
    }

    [Fact]
    public void Subject_ReturnsExpectedSubject()
    {
        // Arrange
        string orderId = "Test order ID";
        OrderConfirmationMessage message = new(orderId);

        // Act
        string result = message.Subject;

        // Assert
        Assert.Equal("Order Confirmation", result);
    }
}
