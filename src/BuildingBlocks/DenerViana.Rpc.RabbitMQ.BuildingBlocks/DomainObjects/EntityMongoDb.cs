using MongoDB.Bson.Serialization.Attributes;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.DomainObjects;

public class EntityMongoDb : AuditMongoDb
{
    #region Properties

    [BsonElement("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    #endregion

    #region Public Methods

    public static bool operator ==(EntityMongoDb a, EntityMongoDb b)
    {
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;

        return a.Equals(b);
    }
    public static bool operator !=(EntityMongoDb a, EntityMongoDb b)
    {
        return !(a == b);
    }
    public override bool Equals(object obj)
    {
        if (obj is EntityMongoDb compareTo)
        {
            return Id != Guid.Empty && compareTo.Id != Guid.Empty && Id.Equals(compareTo.Id);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return (GetType().GetHashCode() * 907) + Id.GetHashCode();
    }
    public override string ToString()
    {
        return $"{GetType().Name} [Id={Id}]";
    }

    #endregion
}
