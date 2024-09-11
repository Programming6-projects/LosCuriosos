namespace DistributionCenter.Infraestructure.Validators.Components.Rules.Concretes;

using DistributionCenter.Infraestructure.Validators.Components.Rules.Interfaces;

public readonly record struct ValidationRule<T>(Func<T, bool> Condition, string ErrorMessage) : IValidationRule<T>;
