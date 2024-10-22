using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.MovieOperations.GetMovieDetail;
using WebApi.DBOperations;

namespace WebApi.Application.ActorOperations.GetActorDetail;
public class GetActorDetailQuery
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;
    public int ActorId { get; set; }
    public GetActorDetailQuery(IMovieDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ActorDetailViewModel Handle()
    {
        var actor = _context.Actors
                        .Where(x => x.IsActive)
                        .Include(x => x.Movies)
                        .Include(x => x.Movies).ThenInclude(x => x.Genres)
                        .Include(x => x.Movies).ThenInclude(x => x.Director)
                        .SingleOrDefault(x => x.Id == ActorId);
        if (actor is null)
            throw new InvalidOperationException("Aktör bulunamadı.");

        ActorDetailViewModel vm = _mapper.Map<ActorDetailViewModel>(actor);
        return vm;
    }


}

public class ActorDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public List<MovieDetailViewModelShort> Movies { get; set; } = new List<MovieDetailViewModelShort>();

}