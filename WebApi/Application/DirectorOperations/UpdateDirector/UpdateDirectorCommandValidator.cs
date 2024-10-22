using FluentValidation;

namespace WebApi.Application.DirectorOperations.UpdateDirector;

public class UpdateDirectorCommandValidator : AbstractValidator<UpdateDirectorCommand>
{
    public UpdateDirectorCommandValidator()
    {
        RuleFor(command => command.DirectorId).GreaterThan(0);
        RuleFor(command => command.Model.Name).NotEmpty();
        RuleFor(command => command.Model.Surname).NotEmpty();
    }
}