using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.UpdateGenre;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.UpdateGenre;

public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;

    public UpdateGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGenreIsNotExist_InvalidOperationException_ShouldBeReturned()
    {
        UpdateGenreCommand command = new UpdateGenreCommand(_context, _mapper);
        command.GenreId = 555;
        command.Model = new UpdateGenreModel() { Name = "tester" };

        FluentActions.Invoking(() => command.Handle())
                                        .Should().Throw<InvalidOperationException>()
                                        .And.Message.Should().Be("Film türü bulunamadı.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
    {
        UpdateGenreModel model = new UpdateGenreModel() { Name = "tester" };
        UpdateGenreCommand command = new UpdateGenreCommand(_context, _mapper);
        command.GenreId = 1;
        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var genre = _context.Genres.FirstOrDefault(x => x.Id == command.GenreId);

        genre.Should().NotBeNull();
        genre.Name.Should().Be(model.Name);


    }
}