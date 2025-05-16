namespace DenerViana.Rpc.RabbitMQ.Users.Api.Application.Models.Response;

public class UserDetailsResponse
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string TaxNumber { get; private set; }
    public bool IsExcluded { get; private set; }
    public AddressDetailsResponse Address { get; private set; } = new AddressDetailsResponse();
}
