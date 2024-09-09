namespace DistributionCenter.Application.Tables.Bases;

using System;
using DistributionCenter.Application.Connections.Interfaces;
using DistributionCenter.Application.QueryCommands.Concretes.Dapper.Concretes;
using DistributionCenter.Application.QueryCommands.Interfaces;
using DistributionCenter.Application.Tables.Interfaces;
using DistributionCenter.Domain.Entities.Interfaces;

public abstract class BaseDapperTable<T>(
    IDbConnectionFactory dbConnectionFactory,
    string tableName,
    string getByIdFields,
    string createFields,
    string createValues,
    string updateFields
) : ITable<T>
    where T : IEntity
{
    protected string TableName { get; } = tableName;
    protected string GetByIdFields { get; } =
        $"id AS Id, {getByIdFields}, is_active AS IsActive, created_at AS CreatedAt, updated_at AS UpdatedAt";
    protected string CreateFields { get; } = $"id, {createFields}, is_active, created_at, updated_at";
    protected string CreateValues { get; } = $"@Id, {createValues}, @IsActive, @CreatedAt, @UpdatedAt";
    protected string UpdateFields { get; } = $"{updateFields}, is_active=@IsActive, updated_at=@UpdatedAt";

    protected IDbConnectionFactory DbConnectionFactory { get; } = dbConnectionFactory;

    public IQuery<T> GetByIdQuery(Guid id)
    {
        return new GetByIdDapperQuery<T>(DbConnectionFactory, TableName, id, GetByIdFields);
    }

    public ICommand CreateCommand(T entity)
    {
        return new CreateDapperCommand<T>(DbConnectionFactory, entity, TableName, CreateFields, CreateValues);
    }

    public ICommand UpdateCommand(T entity)
    {
        return new UpdateDapperCommand<T>(DbConnectionFactory, entity, TableName, UpdateFields);
    }
}
