namespace DistributionCenter.Commons.Errors;

using DistributionCenter.Commons.Enums;
using DistributionCenter.Commons.Errors.Interfaces;

#pragma warning disable CA1716
public readonly record struct Error : IError
{
    public string Code { get; }
    public string Description { get; }
    public ErrorType Type { get; }

    private Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    public static IError Conflict(
        string code = "General.Conflict",
        string description = "A 'Conflict' error has occurred"
    )
    {
        return new Error(code, description, ErrorType.Conflict);
    }

    public static IError Validation(
        string code = "General.Validation",
        string description = "A 'Validation' error has occurred"
    )
    {
        return new Error(code, description, ErrorType.Validation);
    }

    public static IError NotFound(
        string code = "General.NotFound",
        string description = "A 'Not Found' error has occurred"
    )
    {
        return new Error(code, description, ErrorType.NotFound);
    }

    public static IError Unauthorized(
        string code = "General.Unauthorized",
        string description = "A 'Unauthorized' error has occurred"
    )
    {
        return new Error(code, description, ErrorType.Unauthorized);
    }
}
