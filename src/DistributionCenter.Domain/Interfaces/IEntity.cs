namespace DistributionCenter.Domain.Interfaces;

public interface IEntity
{
    Guid Id { get; }
    bool IsActive { get; }
}
