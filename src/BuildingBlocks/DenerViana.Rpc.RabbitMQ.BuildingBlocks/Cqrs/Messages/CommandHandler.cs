using FluentValidation.Results;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Cqrs.Messages;

public abstract class CommandHandler
{
    protected ValidationResult ValidationResult;
    protected CommandHandler()
    {
        ValidationResult = new ValidationResult();
    }

    protected void AddError(string errorMessage)
    {
        ValidationResult.Errors.Add(new ValidationFailure(string.Empty, errorMessage));
    }
}
