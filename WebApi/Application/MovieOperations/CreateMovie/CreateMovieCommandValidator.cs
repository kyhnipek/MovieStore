using FluentValidation;

namespace WebApi.Application.MovieOperations.CreateMovie;

public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(command => command.Model.DirectorId).GreaterThan(0);
        RuleFor(command => command.Model.GenreIds).NotEmpty();
        RuleFor(command => command.Model.ActorIds).NotEmpty();
        RuleFor(command => command.Model.Price).NotEmpty();
        RuleFor(command => command.Model.Year).NotEmpty();
        RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(2);
    }
}