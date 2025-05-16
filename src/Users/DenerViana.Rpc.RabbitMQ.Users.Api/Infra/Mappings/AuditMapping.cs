using DenerViana.Rpc.RabbitMQ.BuildingBlocks.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Infra.Mappings;

public static class AuditMapping
{
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

