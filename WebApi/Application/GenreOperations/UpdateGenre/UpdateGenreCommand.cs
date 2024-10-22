using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.UpdateGenre;

public class UpdateGenreCommand
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;
    public int GenreId { get; set; }
    public UpdateGenreModel Model { get; set; }

    public UpdateGenreCommand(IMovieDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public void Handle()
    {
        var genre = _context.Genres.FirstOrDefault(x => x.Id == GenreId);
        if (genre is null)
            throw new InvalidOperationException("Film türü bulunamadı.");

        genre = _mapper.Map<UpdateGenreModel, Genre>(Model, genre);
        _context.Update(genre);
        _context.SaveChanges();
    }
}

public class UpdateGenreModel
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
}