namespace DistributionCenter.Infraestructure.Validators.Core.Interfaces;

using DistributionCenter.Commons.Results;

public interface IFluentValidator<T>
{
    Result Validate(T value);
}
