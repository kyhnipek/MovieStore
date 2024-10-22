using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
using WebApi.Application.UserOperations.DeleteUser;
using WebApi.Application.UserOperations.Login;
using WebApi.Application.UserOperations.RefreshToken;
using WebApi.Application.UserOperations.SignUp;
using WebApi.DBOperations;
using WebApi.TokenOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]

public class UserController : ControllerBase
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public UserController(IMovieDBContext context, IConfiguration configuration, IMapper mapper)
    {
        _context = context;
        _configuration = configuration;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult SignUp([FromBody] SignUpModel signup)
    {
        SignUpCommand command = new SignUpCommand(_context, _mapper);
        command.Model = signup;

        SignUpCommandValidator validator = new SignUpCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [HttpPost("connect/token")]
    public ActionResult<Token> CreateToken([FromBody] LoginModel login)
    {
        LoginCommand command = new LoginCommand(_context, _configuration);
        command.Model = login;

        LoginCommandValidator validator = new LoginCommandValidator();
        validator.ValidateAndThrow(command);

        var token = command.Handle();
        return token;
    }
    [HttpGet("refreshToken")]
    public ActionResult<Token> RefreshToken([FromQuery] string token)
    {
        RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
        command.RefreshToken = token;
        var resultToken = command.Handle();
        return resultToken;
    }


    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        DeleteUserCommand command = new DeleteUserCommand(_context);
        command.UserId = id;

        DeleteUserCommandValidator validator = new DeleteUserCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    // [HttpGet]
    // public IActionResult GetUsers()
    // {
    //     var usr = _context.Users.Include(x => x.FavoriteGenres).OrderBy(x => x.Id);
    //     return Ok(usr);
    // }
}