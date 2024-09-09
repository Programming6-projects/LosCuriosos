namespace DistributionCenter.Domain.Entities.Concretes;

using DistributionCenter.Domain.Entities.Bases;

public class Client : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
