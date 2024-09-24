namespace DistributionCenter.Services.Tests.Notification;

using DistributionCenter.Domain.Entities.Enums;
using DistributionCenter.Services.Notification.Interfaces;
using Xunit;

public class NotificationFactoryTests
{
    [Theory]
    [InlineData(Status.Pending, typeof(OrderConfirmationMessage))]
    [InlineData(Status.Shipped, typeof(OrderShippedMessage))]
    [InlineData(Status.Delivered, typeof(OrderDeliveredMessage))]
    [InlineData(Status.Cancelled, typeof(OrderCancelledMessage))]
    public void CreateMessage_ReturnsExpectedMessageType(Status status, Type expectedType)
    {
        // Arrange
        Guid orderId = Guid.NewGuid();

        // Act
        IMessage message = NotificationFactory.CreateMessage(status, orderId);

        // Assert
        Assert.IsType(expectedType, message);
    }

    [Fact]
    public void CreateMessage_ThrowsArgumentOutOfRangeException_ForUnsupportedStatus()
    {
        // Arrange
        Guid orderId = Guid.NewGuid();
        Status unsupportedStatus = (Status)999;

        // Act & Assert
        ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => NotificationFactory.CreateMessage(unsupportedStatus, orderId)
        );
        Assert.Equal("Unsupported order status: 999 (Parameter 'status')", exception.Message);
    }
}
