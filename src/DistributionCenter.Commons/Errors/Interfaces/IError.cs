namespace DistributionCenter.Commons.Errors.Interfaces;

using DistributionCenter.Commons.Enums;

public interface IError
{
    string Code { get; }

    string Description { get; }

    ErrorType Type { get; }
}
