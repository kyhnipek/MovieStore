using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.DeleteGenre;

public class DeleteGenreCommand
{
    private readonly IMovieDBContext _context;

    public DeleteGenreCommand(IMovieDBContext context)
    {
        _context = context;
    }

    public int GenreId { get; set; }

    public void Handle()
    {
        var genre = _context.Genres.FirstOrDefault(x => x.Id == GenreId);
        if (genre is null)
            throw new InvalidOperationException("Film türü bulunamadı.");

        genre.IsActive = false;
        _context.Update(genre);
        _context.SaveChanges();
    }

}