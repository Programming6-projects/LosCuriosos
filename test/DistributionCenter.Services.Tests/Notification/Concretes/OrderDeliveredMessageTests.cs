namespace DistributionCenter.Services.Tests.Notification.Concretes;

using DistributionCenter.Services.Notification.Concretes;

public class OrderDeliveredMessageTests
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
                        <h1>Order Delivered</h1>
                    </div>
                    <div class='content'>
                        <p>Your order with ID <strong>{orderId}</strong> has been successfully delivered.</p>
                        <p>We hope you enjoy your purchase!</p>
                    </div>
                    <div class='footer'>
                        <p>Thank you for choosing our service!</p>
                    </div>
                </div>
            </body>
            </html>";
        OrderDeliveredMessage message = new(orderId);

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
        OrderDeliveredMessage message = new(orderId);

        // Act
        string result = message.Subject;

        // Assert
        Assert.Equal("Order Delivered", result);
    }
}
