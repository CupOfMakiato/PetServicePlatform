using Microsoft.EntityFrameworkCore;
using Server.Domain.Entities;

namespace Server.Infrastructure;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }


    #region DbSet
    public DbSet<Category> Category { get; set; }
    public DbSet<SubCategory> SubCategory { get; set; }
    
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>();

        modelBuilder.Entity<SubCategory>();
        
    }

}

