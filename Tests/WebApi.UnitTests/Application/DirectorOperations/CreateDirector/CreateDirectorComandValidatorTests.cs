using FluentAssertions;
using WebApi.Application.DirectorOperations.CreateDirector;

namespace WebApi.UnitTests.Application.DirectorOperations.CreateDirector;

public class CreateDirectorComandValidatorTests
{
    [Theory]
    [InlineData("t", "t")]
    [InlineData("t", "te")]
    [InlineData("te", "t")]
    [InlineData("t", "")]
    [InlineData("", "t")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
    {
        CreateDirectorCommand command = new CreateDirectorCommand(null, null);
        command.Model = new CreateDirectorModel() { Name = name, Surname = surname };

        CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        CreateDirectorCommand command = new CreateDirectorCommand(null, null);
        command.Model = new CreateDirectorModel() { Name = "testName", Surname = "testSurname" };

        CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }

}