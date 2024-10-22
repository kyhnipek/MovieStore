using WebApi.DBOperations;
using WebApi.TokenOperations;

namespace WebApi.Application.UserOperations.Login;

public class LoginCommand
{
    private readonly IMovieDBContext _context;
    private readonly IConfiguration _configuration;

    public LoginCommand(IMovieDBContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public LoginModel Model { get; set; }

    public Token Handle()
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == Model.Email && u.Password == Model.Password);
        if (user is null)
            throw new InvalidOperationException("Email - şifre hatalı.");

        TokenHandler handler = new TokenHandler(_configuration);
        Token token = handler.CreateAccessToken(user);

        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpireDate = token.Expiration.AddDays(1);
        _context.SaveChanges();

        return token;
    }

}

public class LoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}