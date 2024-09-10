namespace DistributionCenter.Application.Tables.Components.QueryCommands.Interfaces;

using DistributionCenter.Commons.Results;

public interface ICommand
{
    Task<Result> ExecuteAsync();
}
