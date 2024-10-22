using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup;

public static class Movies
{
    public static void AddMovies(this IMovieDBContext context)
    {
        var directors = context.Directors.ToDictionary(d => d.Id);
        var genres = context.Genres.ToDictionary(g => g.Id);
        var actors = context.Actors.ToDictionary(a => a.Id);

        context.Movies.AddRange(
            new Movie
            {
                Id = 1,
                Title = "Do Not Disturb",
                Year = new DateTime(2020, 12, 10),
                Genres = new List<Genre>() { genres[1], genres[2] },
                Price = 10,
                Director = directors[1],
                Actors = new List<Actor>() { actors[1], actors[2], actors[3], actors[4], actors[5], actors[6], actors[7], actors[11] }
            },
            new Movie
            {
                Id = 2,
                Title = "Arif V 216",
                Year = new DateTime(2023, 9, 29),
                Genres = new List<Genre>() { genres[1], genres[3], genres[4], genres[5] },
                Price = 10,
                Director = directors[2],
                Actors = new List<Actor>() { actors[1], actors[7], actors[8], actors[9], actors[10], actors[11], actors[12], actors[13], actors[14] }
            },
            new Movie
            {
                Id = 3,
                Title = "Bursa Bülbülü",
                Year = new DateTime(2023, 10, 6),
                Genres = new List<Genre>() { genres[1], genres[2] },
                Price = 10,
                Director = directors[3],
                Actors = new List<Actor>() { actors[15], actors[16], actors[17], actors[18], actors[19], actors[20], actors[21] }
            }

        );
        context.SaveChanges();

    }
}