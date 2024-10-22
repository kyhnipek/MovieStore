using FluentAssertions;
using WebApi.Application.DirectorOperations.DeleteDirector;

namespace WebApi.UnitTests.Application.DirectorOperations.DeleteDirector;




public class DeleteDirectorComandValidatorTests
{
    [Fact]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors()
    {
        DeleteDirectorCommand command = new DeleteDirectorCommand(null);
        command.DirectorId = 0;

        DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        DeleteDirectorCommand command = new DeleteDirectorCommand(null);
        command.DirectorId = 1;

        DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }

}