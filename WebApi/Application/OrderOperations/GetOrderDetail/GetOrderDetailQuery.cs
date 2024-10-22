using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.MovieOperations.GetMovieDetail;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.GetOrderDetail;

public class GetOrderDetailQuery
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;
    public string UserRole { get; set; }
    public int UserId { get; set; }
    public int OrderId { get; set; }

    public GetOrderDetailQuery(IMovieDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public OrderDetailViewModel Handle()
    {
        Expression<Func<Order, bool>> isAdmin = UserRole == "Admin" ? (x => x.IsActive) : (x => x.IsActive && x.UserId == UserId);
        var order = _context.Orders.Where(isAdmin)
                                        .Include(x => x.Movies)
                                        .Include(x => x.User)
                                        .Include(x => x.Movies).ThenInclude(x => x.Director)
                                        .Include(x => x.Movies).ThenInclude(x => x.Genres)
                                        .FirstOrDefault(x => x.Id == OrderId);
        if (order is null)
            throw new InvalidOperationException("Sipariş bulunamadı");

        OrderDetailViewModel vm = _mapper.Map<OrderDetailViewModel>(order);
        return vm;
    }
}

public class OrderDetailViewModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal OrderTotal { get; set; }
    public List<MovieDetailViewModelShort> Movies { get; set; } = new List<MovieDetailViewModelShort>();
}