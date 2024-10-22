using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.UpdateActor;

public class UpdateActorCommand
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;
    public int ActorId { get; set; }
    public UpdateActorModel Model { get; set; }

    public UpdateActorCommand(IMovieDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        var actor = _context.Actors.FirstOrDefault(x => x.Id == ActorId);
        if (actor is null)
            throw new InvalidOperationException("Aktör bulunamadı.");

        actor = _mapper.Map<UpdateActorModel, Actor>(Model, actor);
        _context.Update(actor);
        _context.SaveChanges();

    }
}

public class UpdateActorModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public bool IsActive { get; set; }
}