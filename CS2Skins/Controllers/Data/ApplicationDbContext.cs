using CS2Skins.Models;
using Microsoft.EntityFrameworkCore;

namespace CS2Skins.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Skin> Skins { get; set; }
}