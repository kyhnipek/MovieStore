using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.MovieOperations.GetMovieDetail;
using WebApi.DBOperations;

namespace WebApi.Application.DirectorOperations.GetDirectorDetail;
public class GetDirectorDetailQuery
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;
    public int DirectorId { get; set; }
    public GetDirectorDetailQuery(IMovieDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public DirectorDetailViewModel Handle()
    {
        var director = _context.Directors
                        .Where(x => x.IsActive)
                        .Include(x => x.Movies)
                        .Include(x => x.Movies).ThenInclude(x => x.Genres)
                        .SingleOrDefault(x => x.Id == DirectorId);
        if (director is null)
            throw new InvalidOperationException("Yönetmen bulunamadı.");

        DirectorDetailViewModel vm = _mapper.Map<DirectorDetailViewModel>(director);
        return vm;
    }


}

public class DirectorDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public List<MovieDetailViewModelShort> Movies { get; set; } = new List<MovieDetailViewModelShort>();

}