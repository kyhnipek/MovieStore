using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.OrderOperations.CreateOrder;
using WebApi.Application.OrderOperations.DeleteOrder;
using WebApi.Application.OrderOperations.UpdateOrder;
using WebApi.Application.OrderOperations.GetOrderDetail;
using WebApi.Application.OrderOperations.GetOrders;
using WebApi.DBOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class OrderController : ControllerBase
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;

    public OrderController(IMovieDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetOrders()
    {
        GetOrdersQuery query = new GetOrdersQuery(_context, _mapper);
        query.UserId = GetUserId();
        query.UserRole = GetUserRole();
        var result = query.Handle();
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id}")]
    public IActionResult GetOrderDetail(int id)
    {
        GetOrderDetailQuery query = new GetOrderDetailQuery(_context, _mapper);
        query.OrderId = id;
        query.UserId = GetUserId();
        query.UserRole = GetUserRole();

        GetOrderDetailQueryValidator validator = new GetOrderDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var result = query.Handle();
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public IActionResult CreateOrder([FromBody] CreateOrderModel model)
    {
        CreateOrderCommand command = new CreateOrderCommand(_context);
        command.Model = model;
        command.UserId = GetUserId();

        CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [Authorize]
    [HttpPut("{id}")]
    public IActionResult UpdateOrder(int id, [FromBody] UpdateOrderModel model)
    {
        UpdateOrderCommand command = new UpdateOrderCommand(_context);
        command.UserId = GetUserId();
        command.OrderId = id;
        command.Model = model;
        command.UserRole = GetUserRole();


        UpdateOrderCommandValidator validator = new UpdateOrderCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        DeleteOrderCommand command = new DeleteOrderCommand(_context);
        command.UserId = GetUserId();
        command.OrderId = id;
        command.UserRole = GetUserRole();

        DeleteOrderCommandValidator validator = new DeleteOrderCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    private int GetUserId()
    {
        var claimsIdentity = this.User.Identity as ClaimsIdentity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
        return Convert.ToInt32(userId);
    }
    private string GetUserRole()
    {
        var claimsIdentity = this.User.Identity as ClaimsIdentity;
        var userRole = claimsIdentity.FindFirst(ClaimTypes.Role).Value;
        return userRole;
    }

}
