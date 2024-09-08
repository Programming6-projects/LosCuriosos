namespace DistributionCenter.Commons.Results.Interfaces;

using DistributionCenter.Commons.Errors.Interfaces;

public interface IResult
{
    bool IsSuccess { get; }
    ICollection<IError> Errors { get; }
}

public interface IResult<T> : IResult
{
    T Value { get; }
    TNext Match<TNext>(Func<T, TNext> success, Func<ICollection<IError>, TNext> failure);
    Task<TNext> MatchAsync<TNext>(Func<T, Task<TNext>> success, Func<ICollection<IError>, Task<TNext>> failure);
}
