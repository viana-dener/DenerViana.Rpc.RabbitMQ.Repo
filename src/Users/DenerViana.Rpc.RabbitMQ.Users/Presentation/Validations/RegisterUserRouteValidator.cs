using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Tools;
using DenerViana.Rpc.RabbitMQ.Users.Application.Models.Request;
using FluentValidation;

namespace DenerViana.Rpc.RabbitMQ.Users.Presentation.Validations;

/// <summary>
/// Validador de regras para a requisição de registro de usuário (<see cref="RegisterUserRequest"/>),
/// utilizando FluentValidation. Aplica validações para propriedades do modelo, como nome, e-mail, CPF, senha,
/// confirmação de senha e, condicionalmente, o parâmetro de rota "id" dependendo do método HTTP.
/// </summary>
public class RegisterUserRouteValidator : AbstractValidator<RegisterUserRequest>
{
    #region Builders

    /// <summary>
    /// Inicializa uma nova instância de <see cref="RegisterUserRouteValidator"/> e aplica as regras de validação
    /// com base no contexto da requisição HTTP atual.
    /// </summary>
    public RegisterUserRouteValidator(IHttpContextAccessor context)
    {
        ValidateRoute(context);
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

        RuleFor(model => model.Name)
            .NotEmpty()
            .WithMessage("The field name is required.")
            .MinimumLength(3)
            .WithMessage("The name field must have at least 3 characters.")
            .MaximumLength(255)
            .WithMessage("The name field must have a maximum of 255 characters.");

        RuleFor(model => model.TaxNumber)
            .NotEmpty()
            .WithMessage("The field taxNumber is required.")
            .MaximumLength(9)
            .WithMessage("The taxNumber field must have a maximum of 9 characters.");

        RuleFor(model => model.Email)
            .NotEmpty()
            .WithMessage("The field email is required.")
            .EmailAddress()
            .MinimumLength(5)
            .WithMessage("The email field must have at least 5 characters.")
            .MaximumLength(254)
            .WithMessage("The email field must have a maximum of 254 characters.");

        // Validação para senha forte
        RuleFor(model => model.Password)
            .NotEmpty()
            .WithMessage("The field password is required.")
            .MinimumLength(8)
            .WithMessage("The password field must have at least 8 characters.")
            .MaximumLength(255)
            .WithMessage("The password field must have a maximum of 255 characters.")
            .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
            .WithMessage("The password must contain at least one uppercase letter, one lowercase letter, one number, and one special character (@, $, !, %, *, ?, &).");

        RuleFor(model => model.ConfirmPassword)
            .NotEmpty()
            .WithMessage("The field confirmPassword is required.")
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
        return id.IsGuid();
    }

    #endregion
}
