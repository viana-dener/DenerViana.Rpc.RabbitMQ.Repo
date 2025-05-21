using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Settings;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Entities;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Infra.Context;

/// <summary>
/// Represents the MongoDB database context, providing access to collections.
/// </summary>
public class MongoDbContext : IMongoDbContext
{
    private readonly IMongoDatabase _database;

    public async Task<bool> CommitAsync()
    {
        return await Task.FromResult(true);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MongoDbContext"/> class.
    /// </summary>
    /// <param name="settings">The MongoDB settings configuration.</param>
    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var mongoSettings = MongoClientSettings.FromConnectionString(settings.Value.MongoDbConnection);

        mongoSettings.GuidRepresentation = GuidRepresentation.Standard;

        var client = new MongoClient(mongoSettings);
        _database = client.GetDatabase(settings.Value.DatabaseName);

        Console.WriteLine($"Conectado ao MongoDB: {settings.Value.MongoDbConnection}");
    }

    public IMongoCollection<Client> Clients => _database.GetCollection<Client>("clients");

    #region IDisposable Implementation

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    #endregion
}
