using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.CreateGenre;

public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;

    public CreateGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGenreIsExist_InvalidOperationException_ShouldBeReturn()
    {
        var genre = new Genre() { Name = "genreTestName", };
        _context.Genres.Add(genre);
        _context.SaveChanges();

        CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
        command.Model = new CreateGenreModel() { Name = genre.Name };

        FluentActions.Invoking(() => command.Handle())
                            .Should().Throw<InvalidOperationException>()
                            .And.Message.Should().Be("Film türü zaten mevcut.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
    {
        CreateGenreModel model = new CreateGenreModel() { Name = "genreTestName2" };
        CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();
        var genre = _context.Genres.FirstOrDefault(a => a.Name == model.Name);

        genre.Should().NotBeNull();
        genre.Name.Should().Be(model.Name);
    }


}