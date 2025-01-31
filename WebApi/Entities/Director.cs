using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;
public class Director
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Movie>? Movies { get; set; } = new List<Movie>();
}