using FluentAssertions;
using WebApi.Application.GenreOperations.GetGenreDetail;

namespace WebApi.UnitTests.Application.GenreOperations.GetGenreDetail;

public class GetGenreDetailQueryValidatorTests
{
    [Fact]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError()
    {
        GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
        query.GenreId = 0;

        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
    {
        GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
        query.GenreId = 1;

        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().Be(0);
    }
}
