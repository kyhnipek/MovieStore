using FluentAssertions;
using WebApi.Application.GenreOperations.CreateGenre;

namespace WebApi.UnitTests.Application.GenreOperations.CreateGenre;

public class CreateGenreCommandValidatorTests
{
    [Theory]
    [InlineData("t")]
    [InlineData("")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
    {
        CreateGenreCommand command = new CreateGenreCommand(null, null);
        command.Model = new CreateGenreModel() { Name = name };

        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        CreateGenreCommand command = new CreateGenreCommand(null, null);
        command.Model = new CreateGenreModel() { Name = "testName" };

        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }

}