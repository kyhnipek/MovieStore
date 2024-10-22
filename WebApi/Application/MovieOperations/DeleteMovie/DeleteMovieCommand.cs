using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.MovieOperations.DeleteMovie;

public class DeleteMovieCommand
{
    private readonly IMovieDBContext _context;
    public int MovieId { get; set; }

    public DeleteMovieCommand(IMovieDBContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var movie = _context.Movies.Include(x => x.Director).Include(x => x.Actors).FirstOrDefault(x => x.Id == MovieId);
        if (movie is null)
            throw new InvalidOperationException("Film bulunamadı.");
        movie.IsActive = false;
        _context.SaveChanges();
    }
}