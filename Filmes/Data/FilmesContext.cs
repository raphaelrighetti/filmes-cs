using Filmes.Models;
using Microsoft.EntityFrameworkCore;

namespace Filmes.Data;

public class FilmesContext : DbContext
{
    public FilmesContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
}
