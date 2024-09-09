namespace DistributionCenter.Domain.Entities.Interfaces;

public interface IEntity : IRegister
{
    Guid Id { get; }
    bool IsActive { get; set; }
}
