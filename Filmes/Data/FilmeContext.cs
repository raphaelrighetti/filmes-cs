using Filmes.Models;
using Microsoft.EntityFrameworkCore;

namespace Filmes.Data;

public class FilmeContext : DbContext
{
    public FilmeContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Filme> Filmes { get; set; }
}
