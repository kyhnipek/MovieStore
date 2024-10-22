using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.UnitTests.TestSetup;

public class CommonTestFixture
{
    public MovieDBContext Context { get; set; }
    public IMapper Mapper { get; set; }
    public CommonTestFixture()
    {
        var options = new DbContextOptionsBuilder<MovieDBContext>()
                                .UseInMemoryDatabase(databaseName: "MovieStoreTestDB")
                                .Options;

        Context = new MovieDBContext(options);
        Context.Database.EnsureCreated();

        Context.AddActors();
        Context.AddGenres();
        Context.AddDirectors();
        Context.AddMovies();


        Mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
    }
}