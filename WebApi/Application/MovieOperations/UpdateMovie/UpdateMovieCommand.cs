using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.UpdateMovie;

public class UpdateMovieCommand
{
    private readonly IMovieDBContext _context;
    public UpdateMovieModel Model { get; set; }
    public int MovieId { get; set; }

    public UpdateMovieCommand(IMovieDBContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var movie = _context.Movies.Include(x => x.Director).Include(x => x.Actors).Include(x => x.Genres).FirstOrDefault(x => x.Id == MovieId);
        if (movie is null)
            throw new InvalidOperationException("Film bulunamadı.");

        movie.Genres = Model.GenreIds.Any() ? GetNewGenres() : movie.Genres;
        movie.Actors = Model.ActorIds.Any() ? GetNewActors() : movie.Actors;
        movie.DirectorId = Model.DirectorId != default ? Model.DirectorId : movie.DirectorId;
        movie.IsActive = Model.IsActive != default ? Model.IsActive : movie.IsActive;
        movie.Year = Model.Year != default ? Model.Year : movie.Year;
        movie.Title = Model.Title != default ? Model.Title : movie.Title;
        movie.Price = Model.Price != default ? Model.Price : movie.Price;

        _context.Movies.Update(movie);
        _context.SaveChanges();
    }

    public List<Actor> GetNewActors()
    {
        List<Actor> act = new List<Actor>();
        foreach (var item in Model.ActorIds)
        {
            var actor = _context.Actors.FirstOrDefault(x => x.Id == item);
            if (actor is null)
                throw new InvalidOperationException("Aktör bulunamadı.");

            act.Add(actor);
            Console.WriteLine(actor.Id);
        }
        return act;
    }
    public List<Genre> GetNewGenres()
    {
        List<Genre> gen = new List<Genre>();
        foreach (var item in Model.GenreIds)
        {
            var genre = _context.Genres.FirstOrDefault(x => x.Id == item);
            if (genre is null)
                throw new InvalidOperationException("Film türü bulunamadı.");

            gen.Add(genre);
        }
        return gen;
    }
}

public class UpdateMovieModel
{
    public string Title { get; set; }
    public decimal Price { get; set; }
    public int DirectorId { get; set; }
    public DateTime Year { get; set; }
    public bool IsActive { get; set; }
    public List<int>? GenreIds { get; set; } = new List<int>();
    public List<int>? ActorIds { get; set; } = new List<int>();
}