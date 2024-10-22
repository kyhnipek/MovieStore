using FluentAssertions;
using WebApi.Application.MovieOperations.CreateMovie;

namespace WebApi.UnitTests.Application.MovieOperations.CreateMovie;
public class CreateMovieCommandValidatorTests
{
    [Theory]
    [InlineData("t", 10, 0, null, null)]
    [InlineData("te", 10, 0, null, null)]
    [InlineData("t", null, 0, null, null)]
    [InlineData("te", null, 0, null, null)]
    [InlineData("t", null, 1, null, null)]
    [InlineData("te", null, 1, null, null)]
    [InlineData("Titanik", 10, 1, null, null)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, decimal price, int directorId, List<int> actorIds, List<int> genreIds)
    {
        CreateMovieCommand command = new CreateMovieCommand(null, null);
        command.Model = new CreateMovieModel() { Title = title, Price = price, DirectorId = directorId, ActorIds = actorIds, GenreIds = genreIds };

        CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        CreateMovieCommand command = new CreateMovieCommand(null, null);
        command.Model = new CreateMovieModel() { Title = "Titanik", Price = 10, DirectorId = 1, ActorIds = [1], GenreIds = [1], Year = new DateTime(1980, 12, 12) };

        CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }

}