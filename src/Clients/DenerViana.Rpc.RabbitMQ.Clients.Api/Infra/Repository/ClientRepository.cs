using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Extensions;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Entities;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Interfaces;
using MongoDB.Driver;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Infra.Repository;

public class ClientRepository(IMongoDbContext context) : IClientRepository
{
    #region Properties

    private readonly IMongoDbContext _context = context;

    #endregion

    #region Public Methods

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _context.Clients.Find(_ => true).ToListAsync();
    }
    public async Task<Client> GetByIdAsync(Guid id)
    {
        var filter = Builders<Client>.Filter.Eq(p => p.Id, id);
        return await _context.Clients.Find(filter).FirstOrDefaultAsync();
    }
    public async Task<Client> GetByNameAsync(string name)
    {
        var filter = Builders<Client>.Filter.Eq(p => p.Name, name);
        return await _context.Clients.Find(filter).FirstOrDefaultAsync();
    }
    public async Task<bool> ExistsAsync(Guid id)
    {
        var filter = Builders<Client>.Filter.Eq(p => p.Id, id);
        var product = await _context.Clients.Find(filter).FirstOrDefaultAsync();
        return product != null;
    }
    public async Task<bool> ExistsAsync(string name)
    {
        var filter = Builders<Client>.Filter.Eq(p => p.Name, name);
        var product = await _context.Clients.Find(filter).FirstOrDefaultAsync();
        return product != null;
    }
    public async Task<bool> AddAsync(Client client)
    {
        try
        {
            await _context.Clients.InsertOneAsync(client);
            return true;
        }
        catch (MongoWriteException ex) when (ex.WriteError.Code == 11000)
        {
            throw new DataException("A product with the same ID already exists.", 409);
        }
        catch (Exception ex)
        {
            throw new DataException("An error occurred while adding the product.", 500);
        }
    }

    #endregion
}
