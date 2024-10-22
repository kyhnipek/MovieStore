using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.CreateOrder;

public class CreateOrderCommand
{
    private readonly IMovieDBContext _context;

    public int UserId { get; set; }
    public CreateOrderModel Model { get; set; }

    public CreateOrderCommand(IMovieDBContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        List<Movie> mov = new List<Movie>();
        decimal totalPrice = 0;
        foreach (var item in Model.MovieIds)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.Id == item);
            if (movie is null)
                throw new InvalidOperationException("Film bulunamadı.");

            mov.Add(movie);
            totalPrice += movie.Price;
        }
        Order o = new Order();
        o.Movies = mov;
        o.UserId = UserId;
        o.OrderTotal = totalPrice;
        o.OrderDate = DateTime.Now;
        _context.Add(o);
        _context.SaveChanges();
    }
}

public class CreateOrderModel
{
    public List<int> MovieIds { get; set; } = new List<int>();
}