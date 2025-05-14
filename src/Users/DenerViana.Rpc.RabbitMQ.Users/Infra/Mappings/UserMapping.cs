using DenerViana.Rpc.RabbitMQ.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DenerViana.Rpc.RabbitMQ.Users.Infra.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public const int EmailMaxLength = 254;

    #region Public Methods

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("UserDb");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.Origin).HasColumnType("nvarchar(50)").IsRequired();
        builder.Property(x => x.Name).HasColumnType("nvarchar(255)").IsRequired();
        builder.OwnsOne(x => x.Email, tf =>
        {
            tf.Property(x => x.Address)
              .HasColumnName("Email")
              .HasColumnType($"nvarchar({EmailMaxLength})")
              .IsRequired();

            tf.HasIndex(x => x.Address).IsUnique();
        });
        builder.Navigation(x => x.Email).IsRequired();
        builder.Property(x => x.Password).HasColumnType("nvarchar(255)").IsRequired();
        builder.Property(x => x.IsExcluded).HasColumnType("bit").IsRequired();

        // Informações de auditoria
        AuditMapping.ConfigureAudit(builder);
    }

    #endregion
}
