using AutoMapper;
using FluentAssertions;
using WebApi.Application.ActorOperations.UpdateActor;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ActorOperations.UpdateActor;

public class UpdateActorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;

    public UpdateActorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenActorIsNotExist_InvalidOperationException_ShouldBeReturned()
    {
        UpdateActorCommand command = new UpdateActorCommand(_context, _mapper);
        command.ActorId = 555;
        command.Model = new UpdateActorModel() { Name = "tester", Surname = "tester" };

        FluentActions.Invoking(() => command.Handle())
                                        .Should().Throw<InvalidOperationException>()
                                        .And.Message.Should().Be("Aktör bulunamadı.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Actor_ShouldBeUpdated()
    {
        UpdateActorModel model = new UpdateActorModel() { Name = "tester", Surname = "tester" };
        UpdateActorCommand command = new UpdateActorCommand(_context, _mapper);
        command.ActorId = 1;
        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var actor = _context.Actors.FirstOrDefault(x => x.Id == command.ActorId);

        actor.Should().NotBeNull();
        actor.Name.Should().Be(model.Name);
        actor.Surname.Should().Be(model.Surname);


    }
}