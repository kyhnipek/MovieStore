using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.ActorOperations.GetActors;
using WebApi.Application.DirectorOperations.GetDirectors;
using WebApi.Application.GenreOperations.GetGenres;
using WebApi.DBOperations;

namespace WebApi.Application.MovieOperations.GetMovieDetail;

public class GetMovieDetailQuery
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;
    public int MovieId { get; set; }

    public GetMovieDetailQuery(IMovieDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public MovieDetailViewModel Handle()
    {
        var movie = _context.Movies.Where(x => x.IsActive).Include(x => x.Director).Include(x => x.Actors).Include(x => x.Genres).FirstOrDefault(x => x.Id == MovieId);
        if (movie is null)
            throw new InvalidOperationException("Film bulunamadı.");

        MovieDetailViewModel vm = _mapper.Map<MovieDetailViewModel>(movie);
        MovieDetailViewModelShort vms = _mapper.Map<MovieDetailViewModelShort>(movie);
        return vm;
    }
}

public class MovieDetailViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Year { get; set; }
    public List<GenresViewModel> Genres { get; set; } = new List<GenresViewModel>();
    public DirectorsViewModel Director { get; set; } = new DirectorsViewModel();
    public List<ActorsViewModel> Actors { get; set; } = new List<ActorsViewModel>();
}

public class MovieDetailViewModelShort
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Year { get; set; }
    public List<GenresViewModel> Genres { get; set; } = new List<GenresViewModel>();
    public DirectorsViewModel Director { get; set; } = new DirectorsViewModel();
}