using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.ActorOperations.GetActors;
using WebApi.Application.DirectorOperations.GetDirectors;
using WebApi.Application.GenreOperations.GetGenres;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.GetMovies;

public class GetMoviesQuery
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;

    public GetMoviesQuery(IMovieDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<MoviesViewModel> Handle()
    {
        var movieList = _context.Movies.OrderBy(x => x.Id).Include(x => x.Director).Include(x => x.Actors).Include(x => x.Genres).ToList<Movie>();
        List<MoviesViewModel> vm = _mapper.Map<List<MoviesViewModel>>(movieList);
        return vm;
    }
}

public class MoviesViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Year { get; set; }
    public decimal Price { get; set; }
    public List<GenresViewModel> Genres { get; set; } = new List<GenresViewModel>();
    public DirectorsViewModel Director { get; set; } = new DirectorsViewModel();
    public List<ActorsViewModel> Actors { get; set; } = new List<ActorsViewModel>();
}