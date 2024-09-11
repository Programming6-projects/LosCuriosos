namespace DistributionCenter.Commons.Errors;

using DistributionCenter.Commons.Enums;
using DistributionCenter.Commons.Errors.Interfaces;

#pragma warning disable CA1716
public record struct Error : IError
{
    public string Code { get; set; }
    public string Description { get; set; }
    public ErrorType Type { get; set; }

    private Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    public static Error Conflict(
        string code = "General.Conflict",
        string description = "A 'Conflict' error has occurred"
    )
    {
        return new Error(code, description, ErrorType.Conflict);
    }

    public static Error Validation(
        string code = "General.Validation",
        string description = "A 'Validation' error has occurred"
    )
    {
        return new Error(code, description, ErrorType.Validation);
    }

    public static Error NotFound(
        string code = "General.NotFound",
        string description = "A 'Not Found' error has occurred"
    )
    {
        return new Error(code, description, ErrorType.NotFound);
    }

    public static Error Unauthorized(
        string code = "General.Unauthorized",
        string description = "An 'Unauthorized' error has occurred"
    )
    {
        return new Error(code, description, ErrorType.Unauthorized);
    }

    public static Error Unexpected(
        string code = "General.Unexpected",
        string description = "An 'Unexpected' error has occurred"
    )
    {
        return new Error(code, description, ErrorType.Unexpected);
    }
}
