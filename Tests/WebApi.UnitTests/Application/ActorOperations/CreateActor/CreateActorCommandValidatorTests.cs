using FluentAssertions;
using WebApi.Application.ActorOperations.CreateActor;

namespace WebApi.UnitTests.Application.ActorOperations.CreateActor;

public class CreateActorCommandValidatorTests
{
    [Theory]
    [InlineData("t", "t")]
    [InlineData("t", "te")]
    [InlineData("te", "t")]
    [InlineData("t", "")]
    [InlineData("", "t")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
    {
        CreateActorCommand command = new CreateActorCommand(null, null);
        command.Model = new CreateActorModel() { Name = name, Surname = surname };

        CreateActorCommandValidator validator = new CreateActorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        CreateActorCommand command = new CreateActorCommand(null, null);
        command.Model = new CreateActorModel() { Name = "testName", Surname = "testSurname" };

        CreateActorCommandValidator validator = new CreateActorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }

}