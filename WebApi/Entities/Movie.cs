using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Movie

{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Year { get; set; }
    public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    public decimal Price { get; set; }
    public bool IsActive { get; set; } = true;
    public int DirectorId { get; set; }
    public Director Director { get; set; }
    public ICollection<Actor> Actors { get; set; } = new List<Actor>();

}