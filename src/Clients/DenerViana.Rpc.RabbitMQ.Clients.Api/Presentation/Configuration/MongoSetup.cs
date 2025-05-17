using MongoDB.Bson.Serialization.Conventions;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Presentation.Configuration;

public static class MongoSetup
{
    public static void AddMongoDbConvention()
    {
        var conventionPack = new ConventionPack
        {
            new IgnoreIfNullConvention(true)
        };

        ConventionRegistry.Register("Ignore null values globally", conventionPack, _ => true);
    }
}
