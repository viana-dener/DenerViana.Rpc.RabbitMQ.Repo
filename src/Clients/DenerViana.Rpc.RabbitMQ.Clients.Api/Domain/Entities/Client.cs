using DenerViana.Rpc.RabbitMQ.BuildingBlocks.DomainObjects;
using MongoDB.Bson.Serialization.Attributes;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Entities;

/// <summary>
/// 
/// </summary>
public class Client : EntityMongoDb
{
    #region Properties

    [BsonElement("origin")]
    public string Origin { get; private set; }

    [BsonElement("name")]
    public string Name { get; private set; }

    [BsonElement("email")]
    public string Email { get; private set; }

    [BsonElement("taxNumber")]
    public string TaxNumber { get; private set; }

    [BsonElement("picture")]
    public string Picture { get; private set; }

    [BsonElement("isExcluded")]
    public bool IsExcluded { get; private set; }

    [BsonElement("address")]
    public List<Address> Address { get; private set; }

    #endregion

    #region Builders

    private Client() { }

    [BsonConstructor]
    public Client(Guid id, string origin, string name, string email, string taxNumber, string createdId, string createdBy)
    {
        Id = id;
        Origin = origin;
        Name = name;
        Email = email;
        TaxNumber = taxNumber;
        IsExcluded = false;
        Address = [];
        CreatedId = createdId;
        CreatedBy = createdBy;
        CreatedAt = DateTime.UtcNow;
    }

    #endregion

    #region Public Methods

    public void Exclude() => IsExcluded = true;
    public void SetPicture(string picture) => Picture = picture;
    public void SetAddress(string street, string neighborhood, string city, string region, string country, string postalCode, double? latitude, double? longitude)
    {
        Address.Add(new Address
        {
            Street = street,
            Neighborhood = neighborhood,
            City = city,
            Region = region,
            Country = country,
            PostalCode = postalCode,
            Latitude = latitude,
            Longitude = longitude,
            IsExcluded = false
        });
    }
    public void UpdateEmail(string email) => Email = email;
    public override string ToString() => $"{Name}, {Email}, {TaxNumber}";

    #endregion
}
