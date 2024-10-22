using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.GetGenreDetail;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.GetGenreDetail;

public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;

    public GetGenreDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGenreIsNotExist_InvalidOperation_ExceptionShouldBeReturned()
    {
        GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
        query.GenreId = 999;

        FluentActions.Invoking(() => query.Handle())
                                            .Should().Throw<InvalidOperationException>()
                                            .And.Message.Should().Be("Film türü bulunamadı.");
    }

    [Fact]
    public void WhenGenreIsExist_Genre_ShouldBeReturned()
    {
        var genre = _context.Genres.FirstOrDefault(a => a.Id == 1);
        GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
        query.GenreId = 1;

        GenreDetailViewModel vm = query.Handle();

        vm.Should().NotBeNull();
        vm.Name.Should().Be(genre.Name);
    }
}