namespace DistributionCenter.Commons.Results;

using System.Collections.ObjectModel;
using DistributionCenter.Commons.Errors;
using DistributionCenter.Commons.Errors.Interfaces;
using DistributionCenter.Commons.Results.Interfaces;

public partial class Result : IResult
{
    public static Result Ok()
    {
        return new Result();
    }

    public static implicit operator Result(Error error)
    {
        return new Result(error);
    }

    public static implicit operator Result(Collection<IError> errors)
    {
        return new Result(errors);
    }

    public static implicit operator Result(Error[] errors)
    {
        return new Result([.. errors]);
    }
}

public partial class Result<T> : Result, IResult<T>
{
    public static implicit operator Result<T>(T value)
    {
        return new Result<T>(value);
    }

    public static implicit operator Result<T>(Error error)
    {
        return new Result<T>(error);
    }

    public static implicit operator Result<T>(Collection<IError> errors)
    {
        return new Result<T>(errors);
    }

    public static implicit operator Result<T>(Error[] errors)
    {
        return new Result<T>([.. errors]);
    }
}
