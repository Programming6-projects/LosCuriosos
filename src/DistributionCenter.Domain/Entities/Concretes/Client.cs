namespace DistributionCenter.Domain.Entities.Concretes;

using DistributionCenter.Domain.Entities.Bases;

public class Client : BaseEntity
{
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
}
