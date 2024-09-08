namespace DistributionCenter.Commons.Results;

using DistributionCenter.Commons.Errors.Interfaces;
using DistributionCenter.Commons.Results.Interfaces;

public partial class Result : IResult
{
    private readonly ICollection<IError>? _errors;

    protected Result() { }

    protected Result(IError error)
    {
        _errors = [error];
    }

    protected Result(ICollection<IError> errors)
    {
        _errors = errors;
    }

    public bool IsSuccess => _errors is null || _errors.Count == 0;

    public ICollection<IError> Errors
    {
        get
        {
            if (IsSuccess)
            {
                throw new InvalidOperationException("Cannot access errors when there are no errors.");
            }

            return _errors!;
        }
    }
}

public partial class Result<T> : Result, IResult<T>
{
    private readonly T? _value;

    private Result(T value)
    {
        _value = value;
    }

    private Result(IError error)
        : base(error) { }

    private Result(ICollection<IError> errors)
        : base(errors) { }

    public T Value
    {
        get
        {
            if (!IsSuccess || _value is null)
            {
                throw new InvalidOperationException("Cannot access value when there are errors.");
            }

            return _value;
        }
    }
}
