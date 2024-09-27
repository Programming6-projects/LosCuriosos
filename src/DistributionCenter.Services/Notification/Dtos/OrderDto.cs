namespace DistributionCenter.Services.Notification.Dtos;

using Domain.Entities.Enums;

public class OrderDto
{
    public required Guid OrderId { get; set; }
    public required Status OrderStatus { get; set; }
    public required DateTime TimeToDeliver { get; set; }
}
