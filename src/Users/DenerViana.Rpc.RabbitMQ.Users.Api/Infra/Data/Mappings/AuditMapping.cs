using DenerViana.Rpc.RabbitMQ.BuildingBlocks.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Infra.Data.Mappings;

/// <summary>
/// Provides extension methods to configure audit-related properties for entity mappings.
/// </summary>
public static class AuditMapping
{
    /// <summary>
    /// Configures standard audit fields (CreatedBy, CreatedAt, UpdatedBy, UpdatedAt) for an entity that inherits from <see cref="AuditDb"/>.
    /// </summary>
    /// <typeparam name="TEntity">The entity type that inherits from <see cref="AuditDb"/>.</typeparam>
    /// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/> used to configure the entity.</param>
    public static void ConfigureAudit<TEntity>(EntityTypeBuilder<TEntity> builder) where TEntity : AuditDb
    {
        builder.Property(a => a.CreatedBy)
            .HasColumnName("CreatedBy")
            .HasColumnType("nvarchar(255)")
            .IsRequired();
        builder.Property(a => a.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetime2")
            .IsRequired();
        builder.Property(a => a.UpdatedBy)
            .HasColumnName("UpdatedBy")
            .HasColumnType("nvarchar(255)")
            .IsRequired(false);
        builder.Property(a => a.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .HasColumnType("datetime2")
            .IsRequired(false);
    }
}

