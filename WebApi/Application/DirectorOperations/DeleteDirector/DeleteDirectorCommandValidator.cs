using FluentValidation;

namespace WebApi.Application.DirectorOperations.DeleteDirector;

public class DeleteDirectorCommandValidator : AbstractValidator<DeleteDirectorCommand>
{
    public DeleteDirectorCommandValidator()
    {
        RuleFor(command => command.DirectorId).GreaterThan(0);
    }
}