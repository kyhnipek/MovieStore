using FluentAssertions;
using WebApi.Application.ActorOperations.GetActorDetail;

namespace WebApi.UnitTests.Application.ActorOperations.GetActorDetail;

public class GetActorDetailQueryValidatorTests
{
    [Fact]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError()
    {
        GetActorDetailQuery query = new GetActorDetailQuery(null, null);
        query.ActorId = 0;

        GetActorDetailQueryValidator validator = new GetActorDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
    {
        GetActorDetailQuery query = new GetActorDetailQuery(null, null);
        query.ActorId = 1;

        GetActorDetailQueryValidator validator = new GetActorDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().Be(0);
    }
}