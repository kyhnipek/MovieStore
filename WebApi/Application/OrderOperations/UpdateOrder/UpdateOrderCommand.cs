using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.UpdateOrder;

public class UpdateOrderCommand
{
    private readonly IMovieDBContext _context;
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public string UserRole { get; set; }
    public UpdateOrderModel Model { get; set; }

    public UpdateOrderCommand(IMovieDBContext context)
    {
        _context = context;
    }
    public void Handle()
    {
        Expression<Func<Order, bool>> isAdmin = UserRole == "Admin" ? (x => x.Id == OrderId) : (x => x.Id == OrderId && x.UserId == UserId);
        var order = _context.Orders
                            .Include(x => x.Movies)
                            .SingleOrDefault(isAdmin);
        if (order is null)
            throw new InvalidOperationException("Sipariş bulunamadı.");

        List<Movie> mov = new List<Movie>();
        decimal totalPrice = 0;
        foreach (var item in Model.MovieIds)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.Id == item);
            if (movie is null)
                throw new InvalidOperationException("Film bulunamadı.");

            totalPrice += movie.Price;
            mov.Add(movie);
        }
        order.Movies = mov.Any() ? mov : order.Movies;
        order.IsActive = Model.IsActive != default ? Model.IsActive : order.IsActive;
        order.OrderTotal = mov.Any() ? totalPrice : order.OrderTotal;
        _context.Update(order);
        _context.SaveChanges();
    }

}

public class UpdateOrderModel
{
    public bool IsActive { get; set; }
    public List<int> MovieIds { get; set; } = new List<int>();
}