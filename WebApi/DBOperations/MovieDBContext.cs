using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApi.Entities;

namespace WebApi.DBOperations;

public class MovieDBContext : DbContext, IMovieDBContext
{
    public MovieDBContext(DbContextOptions<MovieDBContext> options) : base(options)
    {

    }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Order> Orders { get; set; }


    public override int SaveChanges()
    {
        return base.SaveChanges();
    }
    public override EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class
    {
        return base.Add(entity);
    }

    public override EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class
    {
        return base.Update(entity);
    }
}