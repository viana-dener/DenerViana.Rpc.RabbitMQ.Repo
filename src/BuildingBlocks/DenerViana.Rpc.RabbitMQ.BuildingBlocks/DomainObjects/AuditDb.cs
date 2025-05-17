using System.ComponentModel.DataAnnotations.Schema;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.DomainObjects;

public class AuditDb
{
    #region Properties

    [NotMapped]
    public Guid? CorrelationId { get; set; }

    public string CreatedId { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }

    #endregion
}
