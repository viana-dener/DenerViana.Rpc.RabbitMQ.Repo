using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Entities;
using MongoDB.Driver;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Interfaces;

public interface IMongoDbContext
{
    IMongoCollection<Client> Clients { get; }
}
