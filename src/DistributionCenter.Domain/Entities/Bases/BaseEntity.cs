namespace DistributionCenter.Domain.Entities.Bases;

using DistributionCenter.Domain.Entities.Interfaces;

public abstract class BaseEntity : BaseRegister, IEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public bool IsActive { get; set; } = true;
}
