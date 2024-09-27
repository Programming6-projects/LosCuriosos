namespace DistributionCenter.Services.Tests.Notification.Concretes;

using DistributionCenter.Services.Notification.Dtos;
using Domain.Entities.Enums;
using Xunit;

public class OrderShippedMessageTests
{
    [Fact]
    public void GetMessage_ReturnsExpectedMessage()
    {
        // Arrange
        Guid orderId = Guid.NewGuid();
        DateTime deliveryTime = new(2024, 10, 1, 14, 30, 0);
        OrderDto order = new()
        {
            OrderId = orderId,
            TimeToDeliver = deliveryTime,
            OrderStatus = Status.Sending
        };

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
                        <p>Your order is on its way! and will arrive on {deliveryTime.Day}-{deliveryTime.Month}-{deliveryTime.Year} at {deliveryTime.Hour}:{deliveryTime.Minute} minutes approximately.</p>
                    </div>
                    <div class='footer'>
                        <p>Thank you for your patience.</p>
                    </div>
                </div>
            </body>
            </html>";

        OrderShippedMessage message = new(order);

        // Act
        string result = message.GetMessage();

        // Assert
        Assert.Equal(expectedMessage.Trim(), result.Trim());
    }

    [Fact]
    public void Subject_ReturnsExpectedSubject()
    {
        // Arrange
        Guid orderId = Guid.NewGuid();
        OrderDto order = new()
        {
            OrderId = orderId,
            OrderStatus = Status.Pending,
            TimeToDeliver = DateTime.Now
        };
        OrderShippedMessage message = new(order);

        // Act
        string result = message.Subject;

        // Assert
        Assert.Equal("Transport Assigned to Your Order", result);
    }
}
