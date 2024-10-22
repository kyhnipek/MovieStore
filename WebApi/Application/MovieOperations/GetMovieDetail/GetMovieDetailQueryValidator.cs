using FluentValidation;

namespace WebApi.Application.MovieOperations.GetMovieDetail;

public class GetMovieDetailQueryValidator : AbstractValidator<GetMovieDetailQuery>
{
    public GetMovieDetailQueryValidator()
    {
        RuleFor(command => command.MovieId).GreaterThan(0);
    }
}