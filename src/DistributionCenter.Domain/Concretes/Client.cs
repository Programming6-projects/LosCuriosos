namespace DistributionCenter.Domain.Concretes;

using DistributionCenter.Domain.Bases;

public class Client : BaseEntity
{
    public required string Name { get; init; }
    public required string LastName { get; init; }
    public required string PhoneNumber { get; init; }
}
