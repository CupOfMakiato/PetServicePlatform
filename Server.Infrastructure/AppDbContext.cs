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
    public DbSet<ApplicationUser> Users  { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<UserService> UserServices { get; set; }
    public DbSet<ServiceCategory> ServiceCategories { get; set; }
    

    
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>();

        modelBuilder.Entity<SubCategory>();
        
    }

}

