namespace DistributionCenter.Infraestructure.Validators.Components.Builders.Concretes;

using System.Collections.ObjectModel;
using DistributionCenter.Commons.Errors;
using DistributionCenter.Commons.Errors.Interfaces;
using DistributionCenter.Infraestructure.Validators.Components.Builders.Interfaces;
using DistributionCenter.Infraestructure.Validators.Components.Rules.Concretes;
using DistributionCenter.Infraestructure.Validators.Components.Rules.Interfaces;

public class ValidationBuilder<T> : IValidationBuilder<T>
{
    private readonly ICollection<IValidationRule<T>> _rules = [];
    private Func<T, bool>? _condition;

    public IValidationBuilder<T> When(Func<T, bool> condition)
    {
        _condition = condition;
        return this;
    }

    public IValidationBuilder<T> AddRule(Func<T, bool> rule, string message)
    {
        _rules.Add(new ValidationRule<T>(rule, message));

        return this;
    }

    public Collection<IError> Validate(T value)
    {
        Collection<IError> errors = [];

        if (_condition != null && !_condition(value))
        {
            return errors;
        }

        foreach (IValidationRule<T> rule in _rules)
        {
            if (!rule.Condition(value))
            {
                errors.Add(Error.Validation(code: "InvalidFieldErrors", description: rule.ErrorMessage));
            }
        }

        return errors;
    }
}
