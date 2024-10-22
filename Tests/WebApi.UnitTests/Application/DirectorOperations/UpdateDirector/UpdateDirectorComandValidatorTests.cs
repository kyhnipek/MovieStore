using FluentAssertions;
using WebApi.Application.DirectorOperations.UpdateDirector;

namespace WebApi.UnitTests.Application.DirectorOperations.UpdateDirector;

public class UpdateDirectorComandValidatorTests
{
    [Theory]
    [InlineData(0, "te", "te")]
    [InlineData(0, "", "")]
    [InlineData(1, "", "")]

    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id, string name, string surname)
    {
        UpdateDirectorCommand command = new UpdateDirectorCommand(null, null);
        command.DirectorId = id;
        command.Model = new UpdateDirectorModel() { Name = name, Surname = surname };

        UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        UpdateDirectorCommand command = new UpdateDirectorCommand(null, null);
        command.DirectorId = 1;
        command.Model = new UpdateDirectorModel() { Name = "tester", Surname = "tester" };

        UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}