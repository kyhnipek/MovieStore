using FluentValidation;

namespace WebApi.Application.MovieOperations.DeleteMovie;

public class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>
{
    public DeleteMovieCommandValidator()
    {
        RuleFor(command => command.MovieId).GreaterThan(0);
    }
}