namespace DistributionCenter.Commons.Results;

using DistributionCenter.Commons.Errors.Interfaces;
using DistributionCenter.Commons.Results.Interfaces;

public partial class Result<T> : Result, IResult<T>
{
    public TNext Match<TNext>(Func<T, TNext> success, Func<ICollection<IError>, TNext> failure)
    {
        ArgumentNullException.ThrowIfNull(success, nameof(success));
        ArgumentNullException.ThrowIfNull(failure, nameof(failure));

        return IsSuccess ? success(Value) : failure(Errors);
    }

    public Task<TNext> MatchAsync<TNext>(Func<T, Task<TNext>> success, Func<ICollection<IError>, Task<TNext>> failure)
    {
        ArgumentNullException.ThrowIfNull(success, nameof(success));
        ArgumentNullException.ThrowIfNull(failure, nameof(failure));

        return IsSuccess ? success(Value) : failure(Errors);
    }
}
