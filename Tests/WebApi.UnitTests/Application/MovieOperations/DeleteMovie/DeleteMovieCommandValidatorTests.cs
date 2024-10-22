using FluentAssertions;
using WebApi.Application.MovieOperations.DeleteMovie;

namespace WebApi.UnitTests.Application.MovieOperations.DeleteMovie;


public class DeleteMovieCommandValidatorTests
{
    [Fact]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors()
    {
        DeleteMovieCommand command = new DeleteMovieCommand(null);
        command.MovieId = 0;

        DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        DeleteMovieCommand command = new DeleteMovieCommand(null);
        command.MovieId = 1;

        DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }

}