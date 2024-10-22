using FluentAssertions;
using WebApi.Application.ActorOperations.UpdateActor;

namespace WebApi.UnitTests.Application.ActorOperations.UpdateActor;

public class UpdateActorCommandValidatorTests
{
    [Theory]
    [InlineData(0, "te", "te")]
    [InlineData(0, "", "")]
    [InlineData(1, "", "")]

    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id, string name, string surname)
    {
        UpdateActorCommand command = new UpdateActorCommand(null, null);
        command.ActorId = id;
        command.Model = new UpdateActorModel() { Name = name, Surname = surname };

        UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        UpdateActorCommand command = new UpdateActorCommand(null, null);
        command.ActorId = 1;
        command.Model = new UpdateActorModel() { Name = "tester", Surname = "tester" };

        UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}