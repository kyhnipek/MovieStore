using FluentAssertions;
using WebApi.Application.ActorOperations.DeleteActor;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ActorOperations.DeleteActor;

public class DeleteActorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly IMovieDBContext _context;

    public DeleteActorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenActorNotExist_InvalidOperationException_ShouldBeReturn()
    {
        DeleteActorCommand command = new DeleteActorCommand(_context);
        command.ActorId = 999;

        FluentActions.Invoking(() => command.Handle())
                                .Should().Throw<InvalidOperationException>()
                                .And.Message.Should().Be("Aktör bulunamadı.");
    }

    [Fact]
    public void WhenValidInputIsGiven_Actor_ShouldBeDeleted()
    {
        DeleteActorCommand command = new DeleteActorCommand(_context);
        command.ActorId = 15;

        FluentActions.Invoking(() => command.Handle()).Invoke();
        var actor = _context.Actors.Where(x => x.IsActive).FirstOrDefault(x => x.Id == command.ActorId);

        actor.Should().BeNull();
    }
}