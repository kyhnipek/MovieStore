using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<Genre> FavoriteGenres { get; set; } = new List<Genre>();
    public Role Role { get; set; } = Role.Customer;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpireDate { get; set; }
    public bool IsActive { get; set; } = true;

}

public enum Role
{
    Admin,
    Customer,
}
