namespace DistributionCenter.Domain.Entities.Enums;

using System.ComponentModel;

public enum Status
{
    [Description("Pending")]
    Pending,

    [Description("Processing")]
    Shipped,

    [Description("Delivered")]
    Delivered,

    [Description("Cancelled")]
    Cancelled,
}
