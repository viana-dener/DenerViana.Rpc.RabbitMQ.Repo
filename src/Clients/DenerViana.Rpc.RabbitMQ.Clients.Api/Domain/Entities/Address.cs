using MongoDB.Bson.Serialization.Attributes;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Entities;

/// <summary>
/// Represents an address with various location details.
/// </summary>
public class Address
{
    #region Properties

    [BsonElement("street")]
    public string Street { get; set; }

    [BsonElement("naighborhood")]
    public string Neighborhood { get; set; }

    [BsonElement("city")]
    public string City { get; set; }

    [BsonElement("region")]
    public string Region { get; set; }

    [BsonElement("country")]
    public string Country { get; set; }

    [BsonElement("postalCode")]
    public string PostalCode { get; set; }

    [BsonElement("latitude")]
    public double? Latitude { get; set; }

    [BsonElement("longitude")]
    public double? Longitude { get; set; }

    [BsonElement("isExcluded")]
    public bool IsExcluded { get; set; }

    #endregion
}
