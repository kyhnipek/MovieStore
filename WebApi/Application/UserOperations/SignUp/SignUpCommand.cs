using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.UserOperations.SignUp;

public class SignUpCommand
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;

    public SignUpCommand(IMovieDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public SignUpModel Model { get; set; }

    public void Handle()
    {
        var user = _context.Users.FirstOrDefault(x => x.Email == Model.Email);
        if (user is not null)
            throw new InvalidOperationException("Kullanıcı zaten mevcut");

        user = _mapper.Map<User>(Model);
        _context.Add(user);
        _context.SaveChanges();
    }

}

public class SignUpModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}