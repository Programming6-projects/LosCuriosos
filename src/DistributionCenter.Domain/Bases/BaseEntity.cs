namespace DistributionCenter.Domain.Bases;

using DistributionCenter.Domain.Interfaces;

public abstract class BaseEntity : BaseRegister, IEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public bool IsActive { get; init; } = true;
}
