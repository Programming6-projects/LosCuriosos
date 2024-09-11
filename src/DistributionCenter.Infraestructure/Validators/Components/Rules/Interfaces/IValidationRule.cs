namespace DistributionCenter.Infraestructure.Validators.Components.Rules.Interfaces;

public interface IValidationRule<T>
{
    string ErrorMessage { get; }
    Func<T, bool> Condition { get; }
}
