namespace DistributionCenter.Infraestructure.Validators.Components.Builders.Interfaces;

using System.Collections.ObjectModel;
using DistributionCenter.Commons.Errors.Interfaces;

public interface IValidationBuilder<T>
{
#pragma warning disable CA1716
    IValidationBuilder<T> When(Func<T, bool> condition);

    IValidationBuilder<T> AddRule(Func<T, bool> rule, string message);

    Collection<IError> Validate(T value);
}
