using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup;

public static class Genres
{
    public static void AddGenres(this IMovieDBContext context)
    {
        context.Genres.AddRange(
                new Genre { Id = 1, Name = "Comedy" },
                new Genre { Id = 2, Name = "Drama" },
                new Genre { Id = 3, Name = "Fantasy" },
                new Genre { Id = 4, Name = "Music" },
                new Genre { Id = 5, Name = "Sci-Fi" }
        );
        context.SaveChanges();
    }

}