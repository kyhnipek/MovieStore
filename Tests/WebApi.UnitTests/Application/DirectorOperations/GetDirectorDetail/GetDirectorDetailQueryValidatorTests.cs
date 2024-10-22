using FluentAssertions;
using WebApi.Application.DirectorOperations.GetDirectorDetail;

namespace WebApi.UnitTests.Application.DirectorOperations.GetDirectorDetail;

public class GetDirectorDetailQueryValidatorTests
{
    [Fact]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError()
    {
        GetDirectorDetailQuery query = new GetDirectorDetailQuery(null, null);
        query.DirectorId = 0;

        GetDirectorDetailQueryValidator validator = new GetDirectorDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
    {
        GetDirectorDetailQuery query = new GetDirectorDetailQuery(null, null);
        query.DirectorId = 1;

        GetDirectorDetailQueryValidator validator = new GetDirectorDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().Be(0);
    }
}