using AutoMapper;
using FluentAssertions;
using WebApi.Application.ActorOperations.GetActorDetail;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ActorOperations.GetActorDetail;

public class GetActorDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;

    public GetActorDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenActorIsNotExist_InvalidOperation_ExceptionShouldBeReturned()
    {
        GetActorDetailQuery query = new GetActorDetailQuery(_context, _mapper);
        query.ActorId = 999;

        FluentActions.Invoking(() => query.Handle())
                                            .Should().Throw<InvalidOperationException>()
                                            .And.Message.Should().Be("Aktör bulunamadı.");
    }

    [Fact]
    public void WhenActorIsExist_Actor_ShouldBeReturned()
    {
        var actor = _context.Actors.FirstOrDefault(a => a.Id == 1);
        GetActorDetailQuery query = new GetActorDetailQuery(_context, _mapper);
        query.ActorId = 1;

        ActorDetailViewModel vm = query.Handle();

        vm.Should().NotBeNull();
        vm.Name.Should().Be(actor.Name);
        vm.Surname.Should().Be(actor.Surname);
    }
}