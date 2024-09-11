namespace DistributionCenter.Commons.Results;

using System.Collections.ObjectModel;
using DistributionCenter.Commons.Errors.Interfaces;
using DistributionCenter.Commons.Results.Interfaces;

public partial class Result : IResult
{
    public TNext Match<TNext>(Func<TNext> success, Func<Collection<IError>, TNext> failure)
    {
        ArgumentNullException.ThrowIfNull(success, nameof(success));
        ArgumentNullException.ThrowIfNull(failure, nameof(failure));

        return IsSuccess ? success() : failure(Errors);
    }

    public Task<TNext> MatchAsync<TNext>(Func<Task<TNext>> success, Func<Collection<IError>, Task<TNext>> failure)
    {
        ArgumentNullException.ThrowIfNull(success, nameof(success));
        ArgumentNullException.ThrowIfNull(failure, nameof(failure));

        return IsSuccess ? success() : failure(Errors);
    }
}

public partial class Result<T> : Result, IResult<T>
{
    public TNext Match<TNext>(Func<T, TNext> success, Func<Collection<IError>, TNext> failure)
    {
        ArgumentNullException.ThrowIfNull(success, nameof(success));
        ArgumentNullException.ThrowIfNull(failure, nameof(failure));

        return IsSuccess ? success(Value) : failure(Errors);
    }

    public Task<TNext> MatchAsync<TNext>(Func<T, Task<TNext>> success, Func<Collection<IError>, Task<TNext>> failure)
    {
        ArgumentNullException.ThrowIfNull(success, nameof(success));
        ArgumentNullException.ThrowIfNull(failure, nameof(failure));

        return IsSuccess ? success(Value) : failure(Errors);
    }
}
