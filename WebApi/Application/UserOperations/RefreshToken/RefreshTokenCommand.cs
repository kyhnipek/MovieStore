using WebApi.DBOperations;
using WebApi.TokenOperations;

namespace WebApi.Application.UserOperations.RefreshToken;

public class RefreshTokenCommand
{
    private readonly IMovieDBContext _context;
    private readonly IConfiguration _configuration;
    public string RefreshToken { get; set; }

    public RefreshTokenCommand(IMovieDBContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public Token Handle()
    {
        var user = _context.Users.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);
        if (user is null)
        {
            throw new InvalidOperationException("Geçerli bir refresh token bulunamadı.");
        }
        TokenHandler handler = new TokenHandler(_configuration);
        Token token = handler.CreateAccessToken(user);

        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
        _context.SaveChanges();
        return token;
    }
}