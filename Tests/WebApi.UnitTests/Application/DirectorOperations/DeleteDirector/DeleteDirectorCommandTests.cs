using FluentAssertions;
using WebApi.Application.DirectorOperations.DeleteDirector;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.DirectorOperations.DeleteDirector;

public class DeleteDirectorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly IMovieDBContext _context;

    public DeleteDirectorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenDirectorNotExist_InvalidOperationException_ShouldBeReturn()
    {
        DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
        command.DirectorId = 999;

        FluentActions.Invoking(() => command.Handle())
                                .Should().Throw<InvalidOperationException>()
                                .And.Message.Should().Be("Yönetmen bulunamadı.");
    }

    [Fact]
    public void WhenValidInputIsGiven_Director_ShouldBeDeleted()
    {
        DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
        command.DirectorId = 1;

        FluentActions.Invoking(() => command.Handle()).Invoke();
        var director = _context.Directors.Where(x => x.IsActive).FirstOrDefault(x => x.Id == command.DirectorId);

        director.Should().BeNull();
    }
}