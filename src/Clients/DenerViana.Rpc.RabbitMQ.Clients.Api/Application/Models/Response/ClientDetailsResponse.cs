using DenerViana.Rpc.RabbitMQ.BuildingBlocks.ValueObjects;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Response;

public class ClientDetailsResponse
{
    #region Properties

    public Guid Id { get; set; }
    public string Name { get;  set; }
    public Email Email { get;  set; }
    public TaxNumber TaxNumber { get;  set; }
    public string Picture { get;  set; }
    public bool IsExcluded { get;  set; }
    public AddressDetailsResponse Address { get;  set; }

    #endregion
}
