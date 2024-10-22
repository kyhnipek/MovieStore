using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.MovieOperations.GetMovieDetail;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.GetOrders;

public class GetOrdersQuery
{
    private readonly IMovieDBContext _context;
    private readonly IMapper _mapper;
    public int UserId { get; set; }
    public string UserRole { get; set; }

    public GetOrdersQuery(IMovieDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<OrdersViewModel> Handle()
    {
        // Sorguyu yapan admin ise tüm siparişleri, müşteri ise sadece kendi siparişlerini görebilir.
        Expression<Func<Order, bool>> isAdmin = UserRole == "Admin" ? (x => x.IsActive) : (x => x.IsActive && x.UserId == UserId);
        var orderList = _context.Orders.Where(isAdmin)
                            .Include(x => x.User)
                            .Include(x => x.Movies).ThenInclude(m => m.Genres)
                            .Include(x => x.Movies).ThenInclude(m => m.Director)
                            .Include(x => x.Movies).ThenInclude(m => m.Actors)
                            .OrderBy(x => x.Id)
                            .ToList<Order>();
        List<OrdersViewModel> vm = _mapper.Map<List<OrdersViewModel>>(orderList);
        return vm;
    }
}

public class OrdersViewModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal OrderTotal { get; set; }
    public List<MovieDetailViewModelShort> Movies { get; set; } = new List<MovieDetailViewModelShort>();
}