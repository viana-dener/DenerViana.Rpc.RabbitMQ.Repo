using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Extensions;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Entities;
using DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Interfaces;
using DenerViana.Rpc.RabbitMQ.Users.Api.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Infra.Data.Repository;

/// <summary>
/// Implements the user repository for data access using Entity Framework Core.
/// Provides methods to query, check existence, add users, and manages the database context lifecycle.
/// </summary>
public class UserRepository(SqlServerDbContext context) : IUserRepository
{
    #region Properties

    private readonly SqlServerDbContext _context = context ?? throw new DataException(nameof(context), 500);

    #endregion

    #region Public Methods

    /// <summary>
    /// Gets the unit of work associated with this repository.
    /// </summary>
    public IUnitOfWork UnitOfWork => _context;

    /// <summary>
    /// Retrieves all users who are not marked as excluded.
    /// </summary>
    /// <returns>A list of active users.</returns>
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.User.AsNoTracking().Where(w => !w.IsExcluded).ToListAsync();
    }

    /// <summary>
    /// Retrieves a user by their unique identifier if not excluded.
    /// </summary>
    /// <param name="id">The user identifier.</param>
    /// <returns>The user if found; otherwise, null.</returns>
    public async Task<User> GetByIdAsync(Guid id)
    {
        return await _context.User.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && !x.IsExcluded);
    }

    /// <summary>
    /// Retrieves a user by their name if not excluded.
    /// </summary>
    /// <param name="name">The user's name.</param>
    /// <returns>The user if found; otherwise, null.</returns>
    public async Task<User> GetByNameAsync(string name)
    {
        return await _context.User.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == name.ToLower().Trim() && !x.IsExcluded);
    }

    /// <summary>
    /// Checks if a user with the given id exists and is not excluded.
    /// </summary>
    /// <param name="id">The user identifier.</param>
    /// <returns>True if the user exists; otherwise, false.</returns>
    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.User.AsNoTracking()
            .Where(x => x.Id == id && !x.IsExcluded)
            .FirstOrDefaultAsync() != null;
    }

    /// <summary>
    /// Checks if a user with the given email exists and is not excluded.
    /// </summary>
    /// <param name="email">The user's email address.</param>
    /// <returns>True if the user exists; otherwise, false.</returns>
    public async Task<bool> ExistsAsync(string email)
    {
        return await _context.User.AsNoTracking()
            .Where(x => x.Email.Address.ToLower().Trim() == email.ToLower().Trim() && !x.IsExcluded)
            .FirstOrDefaultAsync() != null;
    }

    /// <summary>
    /// Adds a new user to the database.
    /// </summary>
    /// <param name="account">The user entity to add.</param>
    /// <returns>True if the operation succeeded; otherwise, false.</returns>
    public async Task<bool> AddAsync(User account)
    {
        await _context.User.AddAsync(account);
        return await _context.SaveChangesAsync() > 0;
    }

    #endregion

    #region IDisposable Implementation

    private bool _disposed;

    /// <summary>
    /// Disposes the repository and releases resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes managed resources if disposing is true.
    /// </summary>
    /// <param name="disposing">True to dispose managed resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context?.Dispose();
            }

            _disposed = true;
        }
    }

    #endregion
}
