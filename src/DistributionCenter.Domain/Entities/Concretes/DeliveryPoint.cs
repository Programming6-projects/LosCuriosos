namespace DistributionCenter.Domain.Entities.Concretes;

using Bases;

public class DeliveryPoint : BaseEntity
{
    public required double Latitude { get; set; }
    public required double Longitude { get; set; }
}
