namespace DistributionCenter.Domain.Entities.Enums;

using System.ComponentModel;

public enum Status
{
    [Description("Pending")]
    Pending,

    [Description("Processing")]
    Sending,

    [Description("Delivered")]
    Delivered,

    [Description("Cancelled")]
    Cancelled,
}
