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
    public DbSet<Booking> Booking { get; set; }
    public DbSet<Feedback> Feedback { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<Service> Service { get; set; }
    public DbSet<Transaction> Transaction { get; set; }
    public DbSet<Payment> Payment { get; set; }
    public DbSet<BillDetail> BillDetail { get; set; }
    public DbSet<UserService> UserService { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(e =>
        {
            e.ToTable("Category");

            e.HasKey(p => p.Id);

            e.Property(p => p.Id)
            .IsRequired()
            .HasMaxLength(50);

            e.Property(p => p.CategoryName)
            .IsRequired()
            .HasMaxLength(50);

            e.Property(p => p.CreationDate)
            .IsRequired()
            .HasDefaultValueSql("getutcdate()");

            e.Property(p => p.CategoryStatus)
            .IsRequired();

        });

        modelBuilder.Entity<SubCategory>(e =>
        {
            e.ToTable("SubCategory");

            e.HasKey(p => p.Id);

            e.Property(p => p.Id)
            .IsRequired()
            .HasMaxLength(50);

            e.Property(p => p.SubName)
            .IsRequired()
            .HasMaxLength(50);

            e.Property(p => p.CreationDate)
            .IsRequired()
            .HasDefaultValueSql("getutcdate()");

            e.Property(p => p.CategoryId)
            .IsRequired();

            e.HasOne(p => p.Category)
            .WithMany(p => p.SubCategories)
            .HasForeignKey(p => p.CategoryId)
            .HasConstraintName("FK_SubCategory_Category");
        });

        modelBuilder.Entity<Role>().HasData(
           new Role { Id = 1, RoleName = "Admin" },
           new Role { Id = 2, RoleName = "User" },
           new Role { Id = 3, RoleName = "Staff" }
        );
        modelBuilder.Entity<Service>()
           .HasOne(c => c.CreatedByUser)
           .WithMany(u => u.ServiceCreated)
           .HasForeignKey(c => c.CreatedByUserId)
           .OnDelete(DeleteBehavior.Restrict);
        // Feedback
        modelBuilder.Entity<Feedback>()
            .HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Feedback>()
            .HasOne(r => r.Service)
            .WithMany()
            .HasForeignKey(r => r.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<UserService>()
            .HasKey(r => new { r.UserId, r.ServiceId });
    }

}

