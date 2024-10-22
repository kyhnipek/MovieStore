using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.ActorOperations.CreateActor;
using WebApi.Application.ActorOperations.DeleteActor;
using WebApi.Application.ActorOperations.UpdateActor;
using WebApi.Application.ActorOperations.GetActorDetail;
using WebApi.Application.ActorOperations.GetActors;
using WebApi.DBOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class ActorController : ControllerBase
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;

    public ActorController(IMovieDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetActors()
    {
        GetActorsQuery query = new GetActorsQuery(_context, _mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetActorDetail(int id)
    {
        GetActorDetailQuery query = new GetActorDetailQuery(_context, _mapper);
        query.ActorId = id;

        GetActorDetailQueryValidator validator = new GetActorDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var result = query.Handle();
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult CreateActor([FromBody] CreateActorModel model)
    {
        CreateActorCommand command = new CreateActorCommand(_context, _mapper);
        command.Model = model;

        CreateActorCommandValidator validator = new CreateActorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public IActionResult UpdateActor(int id, [FromBody] UpdateActorModel model)
    {
        UpdateActorCommand command = new UpdateActorCommand(_context, _mapper);
        command.ActorId = id;
        command.Model = model;

        UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public IActionResult DeleteActor(int id)
    {
        DeleteActorCommand command = new DeleteActorCommand(_context);
        command.ActorId = id;

        DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }


}
