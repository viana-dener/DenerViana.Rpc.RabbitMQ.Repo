using MongoDB.Bson.Serialization.Conventions;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Presentation.Configuration;

/// <summary>
/// Configures MongoDB conventions globally.
/// </summary>
public static class MongoSetup
{
    /// <summary>
    /// Adds a convention to ignore null values in MongoDB serialization.
    /// </summary>
    public static void AddMongoDbConvention()
    {
        var conventionPack = new ConventionPack
        {
            new IgnoreIfNullConvention(true)
        };

        ConventionRegistry.Register("Ignore null values globally", conventionPack, _ => true);
    }
}
