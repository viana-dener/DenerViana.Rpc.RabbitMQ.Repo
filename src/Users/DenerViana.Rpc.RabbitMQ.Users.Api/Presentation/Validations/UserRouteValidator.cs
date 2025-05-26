using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Models.Request;
using FluentValidation;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Presentation.Validations;

/// <summary>
/// Validator for the UserRequest route and model, applying rules based on the current HTTP request context.
/// </summary>
public class UserRouteValidator : AbstractValidator<UserRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserRouteValidator"/> class
    /// and applies validation rules considering the current HTTP method and route data.
    /// </summary>
    /// <param name="context">Accessor to the current HTTP context.</param>
    public UserRouteValidator(IHttpContextAccessor context)
    {
        ValidateRoute(context);
    }

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
                .WithMessage("The id field is required.")
                .MaximumLength(36)
                .WithMessage("The id field must have a maximum of 36 characters.")
                .Must(ValidateGuid)
                .WithMessage("The value entered in the id field is not a valid Guid.");
        }

        RuleFor(model => model.Name)
            .NotEmpty()
            .WithMessage("The name field is required.")
            .MinimumLength(3)
            .WithMessage("The name field must have at least 3 characters.")
            .MaximumLength(255)
            .WithMessage("The name field must have a maximum of 255 characters.");

        RuleFor(model => model.TaxNumber)
            .NotEmpty()
            .WithMessage("The taxNumber field is required.")
            .MaximumLength(9)
            .WithMessage("The taxNumber field must have a maximum of 9 characters.");

        RuleFor(model => model.Email)
            .NotEmpty()
            .WithMessage("The email field is required.")
            .EmailAddress()
            .MinimumLength(5)
            .WithMessage("The email field must have at least 5 characters.")
            .MaximumLength(254)
            .WithMessage("The email field must have a maximum of 254 characters.");

        RuleFor(model => model.Password)
            .NotEmpty()
            .WithMessage("The password field is required.")
            .MinimumLength(8)
            .WithMessage("The password field must have at least 8 characters.")
            .MaximumLength(255)
            .WithMessage("The password field must have a maximum of 255 characters.")
            .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
            .WithMessage("The password must contain at least one uppercase letter, one lowercase letter, one number, and one special character (@, $, !, %, *, ?, &).");

        RuleFor(model => model.ConfirmPassword)
            .NotEmpty()
            .WithMessage("The confirmPassword field is required.")
            .MinimumLength(8)
            .WithMessage("The confirmPassword field must have at least 8 characters.")
            .MaximumLength(255)
            .WithMessage("The confirmPassword field must have a maximum of 255 characters.")
            .Equal(model => model.Password)
            .WithMessage("The password and confirmPassword must match.")
            .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
            .WithMessage("The confirmPassword must contain at least one uppercase letter, one lowercase letter, one number, and one special character (@, $, !, %, *, ?, &).");
    }

    private static bool ValidateGuid(string id)
    {
        return Guid.TryParse(id, out _);
    }
}
