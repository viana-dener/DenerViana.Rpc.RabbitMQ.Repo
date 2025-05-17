using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Settings;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Entities;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Infra.Context;

public class MongoDbContext : IMongoDbContext
{

    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var mongoSettings = MongoClientSettings.FromConnectionString(settings.Value.MongoDbConnection);

        mongoSettings.GuidRepresentation = GuidRepresentation.Standard;

        var client = new MongoClient(mongoSettings);
        _database = client.GetDatabase(settings.Value.DatabaseName);

        // Adicione um log para garantir que a conexão foi estabelecida
        Console.WriteLine($"Conectado ao MongoDB: {settings.Value.MongoDbConnection}");
    }

    public IMongoCollection<Client> Clients => _database.GetCollection<Client>("clients");
}