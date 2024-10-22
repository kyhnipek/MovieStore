using FluentAssertions;
using WebApi.Application.MovieOperations.UpdateMovie;

namespace WebApi.UnitTests.Application.MovieOperations.UpdateMovie;

public class CreateMovieCommandValidatorTests
{
    [Theory]
    [InlineData("t", 10, 0, null, null, 1)]
    [InlineData("te", 10, 0, null, null, 1)]
    [InlineData("t", null, 0, null, null, 1)]
    [InlineData("te", null, 0, null, null, 1)]
    [InlineData("t", null, 1, null, null, 1)]
    [InlineData("te", null, 1, null, null, 1)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, decimal price, int directorId, List<int> actorIds, List<int> genreIds, int id)
    {
        UpdateMovieCommand command = new UpdateMovieCommand(null);
        command.MovieId = id;
        command.Model = new UpdateMovieModel() { Title = title, Price = price, DirectorId = directorId, ActorIds = actorIds, GenreIds = genreIds };

        UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        UpdateMovieCommand command = new UpdateMovieCommand(null);
        command.MovieId = 1;
        command.Model = new UpdateMovieModel() { Title = "Titanik", Price = 10, DirectorId = 1, ActorIds = [1, 2, 3], GenreIds = [1, 2] };

        UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }

}