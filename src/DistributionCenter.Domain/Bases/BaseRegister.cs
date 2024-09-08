namespace DistributionCenter.Domain.Bases;

using DistributionCenter.Domain.Interfaces;

public abstract class BaseRegister : IRegister
{
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public DateTime? UpdatedAt { get; init; }
}
