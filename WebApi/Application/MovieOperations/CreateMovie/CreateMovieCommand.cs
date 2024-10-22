using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.CreateMovie;

public class CreateMovieCommand
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;
    public CreateMovieModel Model { get; set; }

    public CreateMovieCommand(IMovieDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public void Handle()
    {
        var movie = _context.Movies.FirstOrDefault(x => x.Title == Model.Title);
        if (movie is not null)
            throw new InvalidOperationException("Film zaten mevcut.");

        var dir = _context.Directors.FirstOrDefault(x => x.Id == Model.DirectorId);
        Director director = dir == null ? throw new InvalidOperationException("Yönetmen bulunamadı.") : dir;

        List<Actor> act = new List<Actor>();
        foreach (var item in Model.ActorIds)
        {
            var actor = _context.Actors.FirstOrDefault(x => x.Id == item);
            if (actor is null)
                throw new InvalidOperationException("Aktör bulunamadı.");

            act.Add(actor);
        }

        List<Genre> gen = new List<Genre>();
        foreach (var item in Model.GenreIds)
        {
            var genre = _context.Genres.FirstOrDefault(x => x.Id == item);
            if (genre is null)
                throw new InvalidOperationException("Film türü bulunamadı.");

            gen.Add(genre);
        }

        movie = _mapper.Map<Movie>(Model);
        movie.Actors = act;
        movie.Genres = gen;

        _context.Movies.Add(movie);
        _context.SaveChanges();
    }
}

public class CreateMovieModel
{
    public string Title { get; set; }
    public DateTime Year { get; set; }
    public int DirectorId { get; set; }
    public decimal Price { get; set; }
    public List<int> GenreIds { get; set; } = new List<int>();
    public List<int> ActorIds { get; set; } = new List<int>();
}