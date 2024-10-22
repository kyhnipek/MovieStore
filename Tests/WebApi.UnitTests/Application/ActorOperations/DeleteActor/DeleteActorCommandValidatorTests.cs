using FluentAssertions;
using WebApi.Application.ActorOperations.DeleteActor;

namespace WebApi.UnitTests.Application.ActorOperations.DeleteActor;


public class DeleteActorCommandValidatorTests
{
    [Fact]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors()
    {
        DeleteActorCommand command = new DeleteActorCommand(null);
        command.ActorId = 0;

        DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        DeleteActorCommand command = new DeleteActorCommand(null);
        command.ActorId = 1;

        DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }

}