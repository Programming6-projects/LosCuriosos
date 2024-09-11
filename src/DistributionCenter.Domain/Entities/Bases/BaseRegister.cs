namespace DistributionCenter.Domain.Entities.Bases;

using DistributionCenter.Domain.Entities.Interfaces;

public abstract class BaseRegister : IRegister
{
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}
