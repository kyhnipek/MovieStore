using FluentAssertions;
using WebApi.Application.GenreOperations.UpdateGenre;

namespace WebApi.UnitTests.Application.GenreOperations.UpdateGenre;

public class UpdateGenreCommandValidatorTests
{
    [Theory]
    [InlineData(0, "te")]
    [InlineData(0, "")]
    [InlineData(1, "")]

    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id, string name)
    {
        UpdateGenreCommand command = new UpdateGenreCommand(null, null);
        command.GenreId = id;
        command.Model = new UpdateGenreModel() { Name = name };

        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        UpdateGenreCommand command = new UpdateGenreCommand(null, null);
        command.GenreId = 1;
        command.Model = new UpdateGenreModel() { Name = "tester" };

        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}