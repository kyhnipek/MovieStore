using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.MovieOperations.GetMovieDetail;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.GetGenreDetail;

public class GetGenreDetailQuery
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;
    public int GenreId { get; set; }

    public GetGenreDetailQuery(IMovieDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public GenreDetailViewModel Handle()
    {
        var genre = _context.Genres.Where(x => x.IsActive)
                                .Include(x => x.Movies)
                                .Include(x => x.Movies).ThenInclude(x => x.Director)
                                .Include(x => x.Movies).ThenInclude(x => x.Genres)
                                .FirstOrDefault(x => x.Id == GenreId);
        if (genre is null)
            throw new InvalidOperationException("Film türü bulunamadı.");

        GenreDetailViewModel vm = _mapper.Map<GenreDetailViewModel>(genre);
        return vm;
    }
}
public class GenreDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<MovieDetailViewModelShort> Movies { get; set; } = new List<MovieDetailViewModelShort>();
}