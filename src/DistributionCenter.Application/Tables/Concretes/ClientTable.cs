namespace DistributionCenter.Application.Tables.Concretes;

using DistributionCenter.Application.Connections.Interfaces;
using DistributionCenter.Application.Tables.Bases;
using DistributionCenter.Domain.Entities.Concretes;

public class ClientTable(IDbConnectionFactory dbConnectionFactory)
    : BaseDapperTable<Client>(
        dbConnectionFactory,
        tableName: "client",
        getByIdFields: "name, last_name AS LastName, email",
        createFields: "name, last_name, email",
        createValues: "@Name, @LastName, @Email",
        updateFields: "name=@Name, last_name=@LastName, email=@Email"
    ) { }
