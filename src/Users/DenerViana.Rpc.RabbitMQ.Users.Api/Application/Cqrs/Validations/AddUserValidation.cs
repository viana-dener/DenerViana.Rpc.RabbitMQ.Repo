using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Tools;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Cqrs.Commands;
using FluentValidation;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Application.Cqrs.Validations;

public class AddUserValidation : AbstractValidator<AddUserCommand>
{
    public AddUserValidation()
    {
        RuleFor(c => c.Origin)
            .NotEmpty()
            .WithMessage("Origin is required")
            .Length(3, 50)
            .WithMessage("Origin must be between 3 and 50 characters");

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .Length(3, 255)
            .WithMessage("Name must be between 3 and 255 characters");

        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .Must(BeAValidEmail)
            .WithMessage("Invalid email format");

        RuleFor(c => c.TaxNumber)
            .NotEmpty()
            .WithMessage("Tax number is required")
            .Must(BeAValidTaxNumber)
            .WithMessage("Tax number is invalid");

        RuleFor(c => c.CorrelationId)
            .NotEmpty()
            .WithMessage("CorrelationId is required");

        RuleFor(c => c.CreatedId)
            .NotEmpty()
            .WithMessage("CreatedId is required");

        RuleFor(c => c.CreatedBy)
            .NotEmpty()
            .WithMessage("CreatedBy is required");
    }


    /// <summary>
    /// Determines whether the provided tax number is valid.
    /// </summary>
    protected static bool BeAValidTaxNumber(string taxNumber)
    {
        return ExtendedMethods.IsTaxNumberValid(taxNumber);
    }

    /// <summary>
    /// Determines whether the provided email is valid.
    /// </summary>
    protected static bool BeAValidEmail(string email)
    {
        return ExtendedMethods.IsEmailValid(email);
    }
}

