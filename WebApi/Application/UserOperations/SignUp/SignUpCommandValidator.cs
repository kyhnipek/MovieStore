using FluentValidation;

namespace WebApi.Application.UserOperations.SignUp;

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(2);
        RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(2);
        RuleFor(command => command.Model.Email).NotEmpty().EmailAddress();
        RuleFor(command => command.Model.Password).NotEmpty().MinimumLength(6);
    }
}