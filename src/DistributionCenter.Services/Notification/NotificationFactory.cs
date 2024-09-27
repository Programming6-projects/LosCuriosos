namespace DistributionCenter.Services.Notification;

using Concretes;
using Domain.Entities.Enums;
using Dtos;
using Interfaces;

public static class NotificationFactory
{
    private static readonly Dictionary<Status, Func<OrderDto, IMessage>> MessageMap =
        new()
        {
            { Status.Pending, static order => new OrderConfirmationMessage(order) },
            { Status.Sending, static order => new OrderShippedMessage(order) },
            { Status.Delivered, static order => new OrderDeliveredMessage(order) },
            { Status.Cancelled, static order => new OrderCancelledMessage(order) },
        };

    public static IMessage CreateMessage(OrderDto order)
    {
        if (MessageMap.TryGetValue(order.OrderStatus, out Func<OrderDto, IMessage>? messageCreator))
            return messageCreator(order);
        return null!;
    }
}
