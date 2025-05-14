namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.DomainObjects;

public class AuditDb
{
    #region Properties

    public Guid? CorrelationId { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }

    #endregion
}
