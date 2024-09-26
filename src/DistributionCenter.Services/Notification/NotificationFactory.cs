namespace DistributionCenter.Services.Notification;

using Concretes;
using DistributionCenter.Domain.Entities.Enums;
using Interfaces;

public static class NotificationFactory
{
    private static readonly Dictionary<Status, Func<Guid, IMessage>> MessageMap =
        new()
        {
            { Status.Pending, static orderId => new OrderConfirmationMessage(orderId) },
            { Status.Sending, static orderId => new OrderShippedMessage(orderId) },
            { Status.Delivered, static orderId => new OrderDeliveredMessage(orderId) },
            { Status.Cancelled, static orderId => new OrderCancelledMessage(orderId) },
        };

    public static IMessage CreateMessage(Status status, Guid orderId)
    {
        if (MessageMap.TryGetValue(status, out Func<Guid, IMessage>? messageCreator))
        {
            return messageCreator(orderId);
        }

        throw new ArgumentOutOfRangeException(nameof(status), $"Unsupported order status: {status}");
    }
}
