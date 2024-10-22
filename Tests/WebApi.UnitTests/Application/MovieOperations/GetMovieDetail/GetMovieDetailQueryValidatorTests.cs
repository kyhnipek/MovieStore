using FluentAssertions;
using WebApi.Application.MovieOperations.GetMovieDetail;

namespace WebApi.UnitTests.Application.MovieOperations.GetMovieDetail;

public class GetMovieDetailQueryValidatorTests
{
    [Fact]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError()
    {
        GetMovieDetailQuery query = new GetMovieDetailQuery(null, null);
        query.MovieId = 0;

        GetMovieDetailQueryValidator validator = new GetMovieDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
    {
        GetMovieDetailQuery query = new GetMovieDetailQuery(null, null);
        query.MovieId = 1;

        GetMovieDetailQueryValidator validator = new GetMovieDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().Be(0);
    }
}