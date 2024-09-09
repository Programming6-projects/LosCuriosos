namespace DistributionCenter.Application.QueryCommands.Interfaces;

using DistributionCenter.Commons.Results;

public interface ICommand
{
    Task<Result> ExecuteAsync();
}
