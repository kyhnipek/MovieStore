using System.Linq.Expressions;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.DeleteOrder;

public class DeleteOrderCommand
{
    private readonly IMovieDBContext _context;
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public string UserRole { get; set; }

    public DeleteOrderCommand(IMovieDBContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        Expression<Func<Order, bool>> isAdmin = UserRole == "Admin" ? (x => x.IsActive) : (x => x.IsActive && x.UserId == UserId);

        var order = _context.Orders.Where(x => x.IsActive == true).FirstOrDefault(isAdmin);
        if (order is null)
            throw new InvalidOperationException("Sipariş bulunamadı.");

        order.IsActive = false;
        _context.Update(order);
        _context.SaveChanges();
    }


}