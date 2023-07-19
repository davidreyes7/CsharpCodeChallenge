using Microsoft.EntityFrameworkCore;

public class TVShowContext : DbContext
{
    public TVShowContext(DbContextOptions<TVShowContext> options)
        : base(options)
    {
    }

    public DbSet<TVShow> TVShows { get; set; }
}
