using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup;

public static class Actors
{
    public static void AddActors(this IMovieDBContext context)
    {

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
    }
}