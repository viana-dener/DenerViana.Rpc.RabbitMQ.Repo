using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Extensions;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Entities;
using DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Interfaces;
using DenerViana.Rpc.RabbitMQ.Users.Api.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Infra.Repository;

public class UserRepository(SqlServerDbContext context) : IUserRepository
{
    #region Properties

    private readonly SqlServerDbContext _context = context ?? throw new DataException(nameof(context), 500);

    #endregion

    #region Public Methods

    public IUnitOfWork UnitOfWork => _context;

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.User.AsNoTracking().Where(w => !w.IsExcluded).ToListAsync();
    }
    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.User.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && !x.IsExcluded);
    }
    public async Task<User?> GetByNameAsync(string name)
    {
        return await _context.User.AsNoTracking().FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == name.ToLower().Trim() && !x.IsExcluded);
    }
    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.User.AsNoTracking().Where(x => x.Id == id && !x.IsExcluded).FirstOrDefaultAsync() != null;
    }
    public async Task<bool> ExistsAsync(string email)
    {
        return await _context.User.AsNoTracking().Where(x => x.Email.Address.ToLower().Trim() == email.ToLower().Trim() && !x.IsExcluded).FirstOrDefaultAsync() != null;
    }

    public async Task<bool> AddAsync(User account)
    {
        await _context.User.AddAsync(account);
        return await _context.SaveChangesAsync() > 0;
    }

    #endregion

    #region IDisposable Implementation

    private bool _disposed;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

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
