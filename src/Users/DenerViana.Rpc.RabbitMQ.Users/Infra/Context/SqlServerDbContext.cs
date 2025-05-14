using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DenerViana.Rpc.RabbitMQ.Users.Infra.Context;

/// <summary>
/// 
/// </summary>
/// <param name="options"></param>
public class SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : DbContext(options), IUnitOfWork
{

    #region Public Methods

    public DbSet<User> User { get; set; }

    public async Task<bool> CommitAsync()
    {
        return await base.SaveChangesAsync() > 0;
    }

    #endregion

    #region Protected Methods

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(255)");

        foreach (var relarionShip in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetForeignKeys()))
            relarionShip.DeleteBehavior = DeleteBehavior.ClientSetNull;

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlServerDbContext).Assembly);
    }

    #endregion
}
