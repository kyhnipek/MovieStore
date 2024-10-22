using FluentAssertions;
using WebApi.Application.GenreOperations.DeleteGenre;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.DeleteGenre;

public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly IMovieDBContext _context;

    public DeleteGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenGenreNotExist_InvalidOperationException_ShouldBeReturn()
    {
        DeleteGenreCommand command = new DeleteGenreCommand(_context);
        command.GenreId = 999;

        FluentActions.Invoking(() => command.Handle())
                                .Should().Throw<InvalidOperationException>()
                                .And.Message.Should().Be("Film türü bulunamadı.");
    }

    [Fact]
    public void WhenValidInputIsGiven_Genre_ShouldBeDeleted()
    {
        DeleteGenreCommand command = new DeleteGenreCommand(_context);
        command.GenreId = 1;

        FluentActions.Invoking(() => command.Handle()).Invoke();
        var genre = _context.Genres.Where(x => x.IsActive).FirstOrDefault(x => x.Id == command.GenreId);

        genre.Should().BeNull();
    }
}