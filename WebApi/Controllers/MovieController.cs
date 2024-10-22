using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.MovieOperations.CreateMovie;
using WebApi.Application.MovieOperations.DeleteMovie;
using WebApi.Application.MovieOperations.UpdateMovie;
using WebApi.Application.MovieOperations.GetMovieDetail;
using WebApi.Application.MovieOperations.GetMovies;
using WebApi.DBOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class MovieController : ControllerBase
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;

    public MovieController(IMovieDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetMovies()
    {
        GetMoviesQuery query = new GetMoviesQuery(_context, _mapper);
        var result = query.Handle();
        return Ok(result);
    }
    [HttpGet("{id}")]
    public IActionResult GetMovieDetail([FromRoute] int id)
    {
        GetMovieDetailQuery query = new GetMovieDetailQuery(_context, _mapper);
        query.MovieId = id;

        GetMovieDetailQueryValidator validator = new GetMovieDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var result = query.Handle();
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult CreateMovie([FromBody] CreateMovieModel model)
    {
        CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
        command.Model = model;

        CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieModel model)
    {
        UpdateMovieCommand command = new UpdateMovieCommand(_context);
        command.MovieId = id;
        command.Model = model;

        UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public IActionResult DeleteMovie([FromRoute] int id)
    {
        DeleteMovieCommand command = new DeleteMovieCommand(_context);
        command.MovieId = id;

        DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }
}
