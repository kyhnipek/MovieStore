using FluentValidation;

namespace WebApi.Application.MovieOperations.UpdateMovie;

public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidator()
    {
        RuleFor(command => command.MovieId).GreaterThan(0);
        RuleFor(command => command.Model.Title).MinimumLength(4).When(command => command.Model.Title.Trim() != string.Empty);
        // RuleFor(command => command.Model.AuthorId).GreaterThan(0);
    }
}