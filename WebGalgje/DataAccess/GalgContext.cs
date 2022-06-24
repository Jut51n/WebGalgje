using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebGalgje.DataAccess;

public class GalgContext : IdentityDbContext<Player>
{
    public DbSet<Player> Players { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Stat> Stats { get; set; }

    public GalgContext(DbContextOptions options)
        : base(options)
    {

    }
}
