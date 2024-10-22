using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MovieDBContext(serviceProvider.GetRequiredService<DbContextOptions<MovieDBContext>>()))
        {

            if (context.Movies.Any())
            {
                return;
            }

            context.Genres.AddRange(
                new Genre { Id = 1, Name = "Comedy" },
                new Genre { Id = 2, Name = "Drama" },
                new Genre { Id = 3, Name = "Fantasy" },
                new Genre { Id = 4, Name = "Music" },
                new Genre { Id = 5, Name = "Sci-Fi" }
            );
            context.SaveChanges();

            context.Actors.AddRange(

                new Actor { Id = 1, Name = "Cem", Surname = "Yılmaz" },
                new Actor { Id = 2, Name = "Ahsen", Surname = "Eroğlu" },
                new Actor { Id = 3, Name = "Nilperi", Surname = "Şahinkaya" },
                new Actor { Id = 4, Name = "Seda", Surname = "Akman" },
                new Actor { Id = 5, Name = "Bülent", Surname = "Şakrak" },
                new Actor { Id = 6, Name = "Celal Kadri", Surname = "Kinoglu" },
                new Actor { Id = 7, Name = "Zafer", Surname = "Algöz" },
                new Actor { Id = 8, Name = "Özkan", Surname = "Uğur" },
                new Actor { Id = 9, Name = "Ozan", Surname = "Güven" },
                new Actor { Id = 10, Name = "Farah Zeynep", Surname = "Abdullah" },
                new Actor { Id = 11, Name = "Özge", Surname = "Özberk" },
                new Actor { Id = 12, Name = "Kerem", Surname = "Alışık" },
                new Actor { Id = 13, Name = "Çağlar", Surname = "Çorumlu" },
                new Actor { Id = 14, Name = "Seda", Surname = "Bakan" },
                new Actor { Id = 15, Name = "Ata", Surname = "Demirer" },
                new Actor { Id = 16, Name = "Özge", Surname = "Özacar" },
                new Actor { Id = 17, Name = "Tarık", Surname = "Pabuççuoğlu" },
                new Actor { Id = 18, Name = "Cem", Surname = "Gelinoğlu" },
                new Actor { Id = 19, Name = "Celil", Surname = "Nalçakan" },
                new Actor { Id = 20, Name = "Melek", Surname = "Baykal" },
                new Actor { Id = 21, Name = "Toygan", Surname = "Avanoğlu" }
            );
            context.SaveChanges();

            context.Directors.AddRange(

                new Director
                {
                    Id = 1,
                    Name = "Cem",
                    Surname = "Yılmaz",
                },
                new Director
                {
                    Id = 2,
                    Name = "Kıvanç",
                    Surname = "Baruönü",
                },
                new Director
                {
                    Id = 3,
                    Name = "Hakan",
                    Surname = "Algül",
                }
            );
            context.SaveChanges();

            var genres = context.Genres.ToDictionary(g => g.Id);
            var actors = context.Actors.ToDictionary(a => a.Id);
            var directors = context.Directors.ToDictionary(d => d.Id);

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

            context.Users.AddRange(

                new User
                {
                    Id = 1,
                    Name = "Kayhan",
                    Surname = "İpek",
                    Email = "kayhan.ipek@mail.com",
                    Password = "123456",
                    Role = Role.Admin,
                    IsActive = true,
                    FavoriteGenres = new List<Genre>() { genres[1], genres[3], genres[5] }
                },
                new User
                {
                    Id = 2,
                    Name = "test",
                    Surname = "customer",
                    Email = "test.customer@mail.com",
                    Password = "123456",
                    Role = Role.Customer,
                    IsActive = true,
                    FavoriteGenres = new List<Genre>() { genres[2], genres[4] }
                }
            );

            context.SaveChanges();


            var movies = context.Movies.ToDictionary(m => m.Id);

            context.Orders.AddRange(
                new Order
                {
                    Id = 1,
                    UserId = 1,
                    Movies = new List<Movie>() { movies[1], movies[2] },
                    OrderDate = new DateTime(2024, 5, 5, 12, 25, 23),
                    OrderTotal = 20
                },
                new Order
                {
                    Id = 2,
                    UserId = 2,
                    Movies = new List<Movie>() { movies[1], movies[3] },
                    OrderDate = new DateTime(2024, 5, 5, 12, 25, 23),
                    OrderTotal = 20
                }
            );
            context.SaveChanges();
        };
    }
}