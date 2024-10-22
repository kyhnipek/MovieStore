using WebApi.DBOperations;

namespace WebApi.Application.UserOperations.DeleteUser;

public class DeleteUserCommand
{
    private readonly IMovieDBContext _context;

    public DeleteUserCommand(IMovieDBContext context)
    {
        _context = context;
    }

    public int UserId { get; set; }

    public void Handle()
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == UserId);
        if (user is null)
            throw new InvalidOperationException("Kullanıcı bulunamadı.");

        user.IsActive = false;
        _context.Users.Update(user);
        _context.SaveChanges();
    }
}