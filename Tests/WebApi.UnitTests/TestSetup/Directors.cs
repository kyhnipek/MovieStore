using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup;

public static class Directors
{
    public static void AddDirectors(this IMovieDBContext context)
    {
        context.Directors.AddRange(
                new Director { Id = 1, Name = "Cem", Surname = "Yılmaz" },
                new Director { Id = 2, Name = "Kıvanç", Surname = "Baruönü" },
                new Director { Id = 3, Name = "Hakan", Surname = "Algül" }
        );
        context.SaveChanges();
    }
}