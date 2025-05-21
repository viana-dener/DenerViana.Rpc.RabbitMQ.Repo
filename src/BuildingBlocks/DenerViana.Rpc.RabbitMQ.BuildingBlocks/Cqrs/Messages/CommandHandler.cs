using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
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

    protected async Task<ValidationResult> SaveChangeAsync(IUnitOfWork uow)
    {
        if (!await uow.CommitAsync()) AddError("Error saving changes");
        
        return ValidationResult;
    }
}
