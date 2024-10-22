using FluentAssertions;
using WebApi.Application.GenreOperations.DeleteGenre;

namespace WebApi.UnitTests.Application.GenreOperations.DeleteGenre;

public class DeleteGenreCommandValidatorTests
{
    [Fact]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors()
    {
        DeleteGenreCommand command = new DeleteGenreCommand(null);
        command.GenreId = 0;

        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        DeleteGenreCommand command = new DeleteGenreCommand(null);
        command.GenreId = 1;

        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }

}