using AutoMapper;
using FluentAssertions;
using WebApi.Application.DirectorOperations.CreateDirector;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.DirectorOperations.CreateDirector;

public class CreateDirectorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;

    public CreateDirectorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenDirectorIsExist_InvalidOperationException_ShouldBeReturn()
    {
        var director = new Director() { Name = "directorTestName", Surname = "directorTestSurname" };
        _context.Directors.Add(director);
        _context.SaveChanges();

        CreateDirectorCommand command = new CreateDirectorCommand(_context, _mapper);
        command.Model = new CreateDirectorModel() { Name = director.Name, Surname = director.Surname };

        FluentActions.Invoking(() => command.Handle())
                            .Should().Throw<InvalidOperationException>()
                            .And.Message.Should().Be("Yönetmen zaten mevcut.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Director_ShouldBeCreated()
    {
        CreateDirectorModel model = new CreateDirectorModel() { Name = "directorTestName2", Surname = "directorTestSurname2" };
        CreateDirectorCommand command = new CreateDirectorCommand(_context, _mapper);
        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();
        var actor = _context.Directors.FirstOrDefault(a => a.Name == model.Name && a.Surname == model.Surname);

        actor.Should().NotBeNull();
        actor.Name.Should().Be(model.Name);
        actor.Surname.Should().Be(model.Surname);
    }


}