namespace DistributionCenter.Domain.Entities.Interfaces;

public interface IRegister
{
    DateTime CreatedAt { get; }
    DateTime? UpdatedAt { get; set; }
}
