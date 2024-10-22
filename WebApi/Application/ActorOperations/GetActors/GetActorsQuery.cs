using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.GetActors;

public class GetActorsQuery
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;

    public GetActorsQuery(IMovieDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<ActorsViewModel> Handle()
    {
        var actorList = _context.Actors.Where(x => x.IsActive).OrderBy(x => x.Id).ToList<Actor>();
        List<ActorsViewModel> vm = _mapper.Map<List<ActorsViewModel>>(actorList);
        return vm;
    }
}

public class ActorsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}