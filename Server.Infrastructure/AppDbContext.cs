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

        //UserRole
        modelBuilder.Entity<UserRole>()
            .HasOne(u => u.Role)
            .WithMany(u => u.UserRoles);

        modelBuilder.Entity<UserRole>()
            .HasOne(u => u.User)
            .WithMany(u => u.UserRoles);

        modelBuilder.Entity<UserRole>()
            .HasKey(u => u.User_Id);
        
        modelBuilder.Entity<UserRole>()
            .HasKey(u => u.Role_Id);

        //Booking
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.User)
            .WithMany(b => b.Bookings)
            .HasForeignKey(b => b.User_Id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Service)
            .WithMany(b => b.Bookings)
            .HasForeignKey(b => b.Service_Id)
            .OnDelete(DeleteBehavior.Restrict);

        //Feedback
        modelBuilder.Entity<Feedback>()
            .HasOne(f => f.User)
            .WithMany(f => f.Feedbacks)
            .HasForeignKey(f => f.User_Id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Feedback>()
            .HasOne(f => f.Service)
            .WithMany(f => f.Feedbacks)
            .HasForeignKey(f => f.Service_Id)
            .OnDelete(DeleteBehavior.Restrict);

        //Payment
        modelBuilder.Entity<Payment>()
            .HasOne(f => f.User)
            .WithMany(f => f.Payments)
            .HasForeignKey(f => f.User_Id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Payment>()
            .HasOne(f => f.Service)
            .WithMany(f => f.Payments)
            .HasForeignKey(f => f.Service_Id)
            .OnDelete(DeleteBehavior.Restrict);

        //UserService
        modelBuilder.Entity<UserService>()
            .HasOne(f => f.User)
            .WithMany(f => f.UserServices)
            .HasForeignKey(f => f.User_Id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserService>()
            .HasOne(f => f.Service)
            .WithMany(f => f.UserServices)
            .HasForeignKey(f => f.Service_Id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserService>()
            .HasKey(u => u.User_Id);

        modelBuilder.Entity<UserService>()
            .HasKey(u => u.Service_Id);

        //ServiceCategory
        modelBuilder.Entity<ServiceCategory>()
            .HasOne(f => f.Service)
            .WithMany(f => f.ServiceCategories)
            .HasForeignKey(f => f.Service_Id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ServiceCategory>()
            .HasOne(f => f.Category)
            .WithMany(f => f.ServiceCategories)
            .HasForeignKey(f => f.Category_Id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ServiceCategory>()
            .HasKey(s => s.Service_Id);
        
        modelBuilder.Entity<ServiceCategory>()
            .HasKey(s => s.Category_Id);

    }

}

