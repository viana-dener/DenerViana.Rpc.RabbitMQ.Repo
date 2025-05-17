using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Tools;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Request;
using FluentValidation;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Presentation.Validations;

public class ClientRouteValidator : AbstractValidator<ClientRequest>
{
    #region Constructors

    public ClientRouteValidator(IHttpContextAccessor context)
    {
        ValidateRoute(context);
        ValidateFields();
    }

    #endregion

    #region Private Methods

    private void ValidateRoute(IHttpContextAccessor context)
    {
        if (context.HttpContext?.Request.Method == HttpMethod.Get.Method ||
            context.HttpContext?.Request.Method == HttpMethod.Put.Method ||
            context.HttpContext?.Request.Method == HttpMethod.Patch.Method ||
            context.HttpContext?.Request.Method == HttpMethod.Delete.Method)
        {
            var routeData = context.HttpContext.GetRouteData();
            var id = routeData.Values["id"]?.ToString();

            RuleFor(x => id)
                .NotEmpty()
                .WithMessage("The field id is required.")
                .MaximumLength(36)
                .WithMessage("The id field must have a maximum of 36 characters.")
                .Must(ValidateGuid)
                .WithMessage("The value entered in the field is not a valid Guid.");
        }
    }

    private void ValidateFields()
    {
        RuleFor(model => model.Name)
            .NotEmpty()
            .WithMessage("The field name is required.")
            .MinimumLength(3)
            .WithMessage("The name field must have at least 3 characters.")
            .MaximumLength(255)
            .WithMessage("The name field must have a maximum of 255 characters.");

        RuleFor(model => model.Email)
            .NotEmpty()
            .WithMessage("The field email is required.")
            .EmailAddress()
            .MinimumLength(5)
            .WithMessage("The email field must have at least 5 characters.")
            .MaximumLength(254)
            .WithMessage("The email field must have a maximum of 254 characters.");

        RuleFor(model => model.TaxNumber)
            .NotEmpty()
            .WithMessage("The field taxNumber is required.")
            .MaximumLength(9)
            .WithMessage("The taxNumber field must have a maximum of 9 characters.");
    }

    private static bool ValidateGuid(string id)
    {
        return id.IsGuid();
    }

    #endregion
}
