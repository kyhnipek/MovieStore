using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.GetGenres;

public class GetGenresQuery
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;

    public GetGenresQuery(IMovieDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<GenresViewModel> Handle()
    {
        var genreList = _context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id).ToList<Genre>();
        List<GenresViewModel> vm = _mapper.Map<List<GenresViewModel>>(genreList);
        return vm;
    }
}

public class GenresViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}