namespace DistributionCenter.Domain.Entities.Concretes;

using Bases;
using Commons.Enums;

public class ClientOrder : BaseEntity
{
    public required Status Status { get; set; }
    public required Guid RouteId { get; set; }
    public required Guid ClientId { get; set; }
    public required Guid DeliveryPointId { get; set; }
    public required IReadOnlyList<ClientOrderProduct> ClientOrderProducts { get; set; }
}
