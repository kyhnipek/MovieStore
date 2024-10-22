using AutoMapper;
using FluentAssertions;
using WebApi.Application.ActorOperations.CreateActor;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ActorOperations.CreateActor;

public class CreateActorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;

    public CreateActorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenActorIsExist_InvalidOperationException_ShouldBeReturn()
    {
        var actor = new Actor() { Name = "actorTestName", Surname = "actorTestSurname" };
        _context.Actors.Add(actor);
        _context.SaveChanges();

        CreateActorCommand command = new CreateActorCommand(_context, _mapper);
        command.Model = new CreateActorModel() { Name = actor.Name, Surname = actor.Surname };

        FluentActions.Invoking(() => command.Handle())
                            .Should().Throw<InvalidOperationException>()
                            .And.Message.Should().Be("Aktör zaten mevcut.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Actor_ShouldBeCreated()
    {
        CreateActorModel model = new CreateActorModel() { Name = "actorTestName2", Surname = "actorTestSurname2" };
        CreateActorCommand command = new CreateActorCommand(_context, _mapper);
        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();
        var actor = _context.Actors.FirstOrDefault(a => a.Name == model.Name && a.Surname == model.Surname);

        actor.Should().NotBeNull();
        actor.Name.Should().Be(model.Name);
        actor.Surname.Should().Be(model.Surname);
    }


}