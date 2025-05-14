using MongoDB.Bson.Serialization.Attributes;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.DomainObjects;

public class AuditMongoDb
{
    #region Properties

    [BsonIgnore]
    public Guid? CorrelationId { get; set; }

    [BsonElement("createdBy")]
    public string CreatedBy { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updatedBy")]
    public string UpdatedBy { get; set; }

    [BsonElement("updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    #endregion
}
