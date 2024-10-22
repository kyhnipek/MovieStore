using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.CreateActor;

public class CreateActorCommand
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;
    public CreateActorModel Model { get; set; }

    public CreateActorCommand(IMovieDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        var actor = _context.Actors.FirstOrDefault(x => x.Name == Model.Name && x.Surname == Model.Surname);
        if (actor is not null)
            throw new InvalidOperationException("Aktör zaten mevcut.");

        actor = _mapper.Map<Actor>(Model);
        _context.Add(actor);
        _context.SaveChanges();
    }
}

public class CreateActorModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
}