namespace DenerViana.Rpc.RabbitMQ.Users.Api.Application.Cqrs.Commands
{
    public class UserCommandHandler
    {
        public void Handle(AddUserCommand command)
        {
            // Validate command

            // Save to database
            //var client = new Client(command.Id, command.Origin, command.Name, command.Email, command.TaxNumber, command.CreatedId, command.CreatedBy);


        }
    }
}
