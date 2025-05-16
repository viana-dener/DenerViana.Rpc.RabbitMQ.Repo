namespace DenerViana.Rpc.RabbitMQ.Users.Api.Application.Models.Request;

public class RegisterUserRequest
{
    public string Name { get; set; }

    public string TaxNumber { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }
}
