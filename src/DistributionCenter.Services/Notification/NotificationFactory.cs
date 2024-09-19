namespace DistributionCenter.Services.Notification;

using Concretes;
using Interfaces;
using System;
using System.Collections.Generic;
using Enums;

public static class NotificationFactory
{
    private static readonly Dictionary<OrderStatus, Func<string, IMessage>> MessageMap =
        new()
        {
            { OrderStatus.Pending, orderId => new OrderConfirmationMessage(orderId) },
            { OrderStatus.Shipped, orderId => new OrderShippedMessage(orderId) },
            { OrderStatus.Delivered, orderId => new OrderDeliveredMessage(orderId) },
            { OrderStatus.Cancelled, orderId => new ErrorNotificationMessage(orderId) }
        };

    public static IMessage CreateMessage(OrderStatus status, string orderId)
    {
        if (MessageMap.TryGetValue(status, out Func<string, IMessage>? messageCreator))
        {
            return messageCreator(orderId);
        }

        throw new ArgumentOutOfRangeException(nameof(status), $"Unsupported order status: {status}");
    }
}
