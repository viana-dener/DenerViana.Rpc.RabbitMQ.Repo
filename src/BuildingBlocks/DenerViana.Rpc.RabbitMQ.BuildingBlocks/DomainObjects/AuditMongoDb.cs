using MongoDB.Bson.Serialization.Attributes;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.DomainObjects;

public class AuditMongoDb
{
    #region Properties

    [BsonIgnore]
    public Guid? CorrelationId { get; set; }

    [BsonElement("createdId")]
    public string CreatedId { get; set; }
    
    [BsonElement("createdBy")]
    public string CreatedBy { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updatedBy")]
    [BsonIgnoreIfNull]
    public string UpdatedBy { get; set; }

    [BsonElement("updatedAt")]
    [BsonIgnoreIfNull]
    public DateTime? UpdatedAt { get; set; }

    #endregion
}
