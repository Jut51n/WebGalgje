using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebGalgje.Entities;

namespace WebGalgje.DataAccess;

public class GalgContext : IdentityDbContext<Player>
{
    public DbSet<Player> Players { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Stat> Stats { get; set; }
    public DbSet<Woord> Words { get; set; }

    public GalgContext(DbContextOptions options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Player>()
            .HasIndex(c => c.UserName)
            .IsUnique();

    }
}
