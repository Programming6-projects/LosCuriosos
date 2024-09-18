namespace DistributionCenter.Domain.Entities.Concretes;

using Bases;
using Commons.Enums;

public class Trip : BaseEntity
{
    public required Status Status { get; set; }
    public required Guid? TransportId { get; set; }
}
