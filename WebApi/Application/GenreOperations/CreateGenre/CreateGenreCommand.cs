using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.CreateGenre;

public class CreateGenreCommand
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;
    public CreateGenreModel Model { get; set; }
    public CreateGenreCommand(IMovieDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        var genre = _context.Genres.FirstOrDefault(x => x.Name == Model.Name);
        if (genre is not null)
            throw new InvalidOperationException("Film türü zaten mevcut.");

        genre = _mapper.Map<Genre>(Model);
        _context.Add(genre);
        _context.SaveChanges();
    }
}
public class CreateGenreModel
{
    public string Name { get; set; }
}