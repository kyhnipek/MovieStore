using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Order
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int? UserId { get; set; }
    public User User { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public decimal? OrderTotal { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Movie> Movies { get; set; } = new List<Movie>();

}