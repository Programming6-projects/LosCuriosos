namespace DistributionCenter.Infraestructure.Validators.Core.Bases;

using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;
using DistributionCenter.Commons.Errors.Interfaces;
using DistributionCenter.Commons.Results;
using DistributionCenter.Infraestructure.Validators.Components.Builders.Concretes;
using DistributionCenter.Infraestructure.Validators.Components.Builders.Interfaces;
using DistributionCenter.Infraestructure.Validators.Core.Interfaces;

public abstract class BaseFluentValidator<T> : IFluentValidator<T>
{
    private readonly Dictionary<string, object> _rules = [];

    protected IValidationBuilder<TValue> RuleFor<TValue>(Expression<Func<T, TValue>> expression)
    {
        ArgumentNullException.ThrowIfNull(expression, nameof(expression));

        string fieldName = expression.Body.ToString().Split('.').Last();

        if (!_rules.TryGetValue(fieldName, out object? value))
        {
            value = new ValidationBuilder<TValue>();
            _rules.Add(fieldName, value);
        }

        return (IValidationBuilder<TValue>)value;
    }

    public Result Validate(T value)
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));

        List<IError> errors = [];

        foreach (KeyValuePair<string, object> rule in _rules)
        {
            string fieldName = rule.Key;
            object validationBuilder = rule.Value;

            object fieldValue = value.GetType().GetProperty(fieldName)!.GetValue(value)!;

            MethodInfo method = validationBuilder.GetType().GetMethod("Validate")!;

            if (method.Invoke(validationBuilder, [fieldValue]) is Collection<IError> errorCollection)
            {
                errors.AddRange(errorCollection);
            }
        }

        if (errors.Count > 0)
        {
            Collection<IError> collectionErrors = [.. errors];

            return collectionErrors;
        }

        return Result.Ok();
    }

    protected IValidationBuilder<IEnumerable<TItem>> RuleForEach<TItem>(Expression<Func<T, IEnumerable<TItem>>> expression, IFluentValidator<TItem> itemValidator)
    {
        ArgumentNullException.ThrowIfNull(expression, nameof(expression));

        string fieldName = expression.Body.ToString().Split('.').Last();

        return RuleFor(expression).AddRule(items =>
            {
                foreach (TItem? item in items)
                {
                    Result result = itemValidator.Validate(item);
                    if (!result.IsSuccess)
                    {
                        return false;
                    }
                }
                return true;
            }, $"One or more items in {fieldName} are invalid.");
    }
}
