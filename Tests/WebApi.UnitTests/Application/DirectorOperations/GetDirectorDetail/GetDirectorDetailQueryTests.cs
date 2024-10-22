using AutoMapper;
using FluentAssertions;
using WebApi.Application.DirectorOperations.GetDirectorDetail;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.DirectorOperations.GetDirectorDetail;

public class GetDirectorDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;

    public GetDirectorDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenDirectorIsNotExist_InvalidOperation_ExceptionShouldBeReturned()
    {
        GetDirectorDetailQuery query = new GetDirectorDetailQuery(_context, _mapper);
        query.DirectorId = 999;

        FluentActions.Invoking(() => query.Handle())
                                            .Should().Throw<InvalidOperationException>()
                                            .And.Message.Should().Be("Yönetmen bulunamadı.");
    }

    [Fact]
    public void WhenDirectorIsExist_Director_ShouldBeReturned()
    {
        var director = _context.Directors.FirstOrDefault(a => a.Id == 1);
        GetDirectorDetailQuery query = new GetDirectorDetailQuery(_context, _mapper);
        query.DirectorId = 1;

        DirectorDetailViewModel vm = query.Handle();

        vm.Should().NotBeNull();
        vm.Name.Should().Be(director.Name);
        vm.Surname.Should().Be(director.Surname);
    }
}