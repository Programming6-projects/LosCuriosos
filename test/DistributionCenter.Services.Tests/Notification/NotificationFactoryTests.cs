namespace DistributionCenter.Services.Tests.Notification;

using DistributionCenter.Domain.Entities.Enums;
using DistributionCenter.Services.Notification.Interfaces;
using DistributionCenter.Services.Notification.Dtos;
using Xunit;

public class NotificationFactoryTests
{
    [Theory]
    [InlineData(Status.Pending, typeof(OrderConfirmationMessage))]
    [InlineData(Status.Sending, typeof(OrderShippedMessage))]
    [InlineData(Status.Delivered, typeof(OrderDeliveredMessage))]
    [InlineData(Status.Cancelled, typeof(OrderCancelledMessage))]
    public void CreateMessage_ReturnsExpectedMessageType(Status status, Type expectedType)
    {
        // Arrange
        OrderDto order = new()
        {
            OrderId = Guid.NewGuid(),
            OrderStatus = status,
            TimeToDeliver = DateTime.Now
        };

        // Act
        IMessage message = NotificationFactory.CreateMessage(order);

        // Assert
        Assert.IsType(expectedType, message);
    }

    [Fact]
    public void CreateMessage_ReturnsNull_ForUnsupportedStatus()
    {
        // Arrange
        OrderDto order = new()
        {
            OrderId = Guid.NewGuid(),
            OrderStatus = (Status)999,
            TimeToDeliver = DateTime.Now
        };

        // Act
        IMessage message = NotificationFactory.CreateMessage(order);

        // Assert
        Assert.Null(message);
    }
}
