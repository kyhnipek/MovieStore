using WebApi.DBOperations;

namespace WebApi.Application.ActorOperations.DeleteActor;

public class DeleteActorCommand
{
    private readonly IMovieDBContext _context;
    public int ActorId { get; set; }

    public DeleteActorCommand(IMovieDBContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var actor = _context.Actors.FirstOrDefault(x => x.Id == ActorId);
        if (actor is null)
            throw new InvalidOperationException("Aktör bulunamadı.");

        actor.IsActive = false;
        _context.Update(actor);
        _context.SaveChanges();
    }
}