namespace DistributionCenter.Commons.Results.Interfaces;

using System.Collections.ObjectModel;
using DistributionCenter.Commons.Errors.Interfaces;

public interface IResult
{
    bool IsSuccess { get; }
    Collection<IError> Errors { get; }

    TNext Match<TNext>(Func<TNext> success, Func<Collection<IError>, TNext> failure);
}

public interface IResult<T> : IResult
{
    T Value { get; }

    TNext Match<TNext>(Func<T, TNext> success, Func<Collection<IError>, TNext> failure);
    Task<TNext> MatchAsync<TNext>(Func<T, Task<TNext>> success, Func<Collection<IError>, Task<TNext>> failure);
}
