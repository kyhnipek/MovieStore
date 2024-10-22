using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.DirectorOperations.DeleteDirector;

public class DeleteDirectorCommand
{
    private readonly IMovieDBContext _context;
    public int DirectorId { get; set; }

    public DeleteDirectorCommand(IMovieDBContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var director = _context.Directors.Where(x => x.IsActive).FirstOrDefault(x => x.Id == DirectorId);
        if (director is null)
            throw new InvalidOperationException("Yönetmen bulunamadı.");

        director.IsActive = false;
        _context.Update(director);
        _context.SaveChanges();
    }
}