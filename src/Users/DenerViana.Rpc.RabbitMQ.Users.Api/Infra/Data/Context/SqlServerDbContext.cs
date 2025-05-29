using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Cqrs.Messages;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Infra.Data.Context;

/// <summary>
/// Represents the Entity Framework Core database context for SQL Server.
/// Manages the User DbSet and configures model properties, including
/// default string column types and foreign key delete behaviors.
/// Implements the Unit of Work pattern to commit transactions.
/// </summary>
public class SqlServerDbContext : DbContext, IUnitOfWork
{

    public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
    {
    }

    #region Public Methods

    /// <summary>
    /// Gets or sets the Users table.
    /// </summary>
    public DbSet<User> User { get; set; }

    /// <summary>
    /// Commits changes asynchronously to the database.
    /// </summary>
    /// <returns>True if one or more changes were saved; otherwise, false.</returns>
    public async Task<bool> CommitAsync()
    {
        return await base.SaveChangesAsync() > 0;
    }

    #endregion

    #region Protected Methods

    /// <summary>
    /// Configures the model and mappings for the database schema.
    /// Sets default column types for string properties and configures
    /// foreign key delete behavior to ClientSetNull.
    /// Applies all entity configurations in the assembly.
    /// </summary>
    /// <param name="modelBuilder">The model builder instance.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<Event>();

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
        {
            property.SetColumnType("varchar(255)");
        }

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlServerDbContext).Assembly);
    }

    #endregion
}
