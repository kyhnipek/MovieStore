using AutoMapper;
using FluentAssertions;
using WebApi.Application.DirectorOperations.UpdateDirector;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.DirectorOperations.UpdateDirector;

public class UpdateDirectorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;

    public UpdateDirectorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenDirectorIsNotExist_InvalidOperationException_ShouldBeReturned()
    {
        UpdateDirectorCommand command = new UpdateDirectorCommand(_context, _mapper);
        command.DirectorId = 555;
        command.Model = new UpdateDirectorModel() { Name = "tester", Surname = "tester" };

        FluentActions.Invoking(() => command.Handle())
                                        .Should().Throw<InvalidOperationException>()
                                        .And.Message.Should().Be("Yönetmen bulunamadı.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Director_ShouldBeUpdated()
    {
        UpdateDirectorModel model = new UpdateDirectorModel() { Name = "tester", Surname = "tester" };
        UpdateDirectorCommand command = new UpdateDirectorCommand(_context, _mapper);
        command.DirectorId = 1;
        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var director = _context.Directors.FirstOrDefault(x => x.Id == command.DirectorId);

        director.Should().NotBeNull();
        director.Name.Should().Be(model.Name);
        director.Surname.Should().Be(model.Surname);


    }
}