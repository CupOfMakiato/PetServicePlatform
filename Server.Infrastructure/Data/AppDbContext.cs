using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.Application.Enum;
using Server.Contracts.Enum;
using Server.Domain.Entities;

namespace Server.Infrastructure.Data;

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
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<ShopData> ShopDatas { get; set; }

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
        modelBuilder.Entity<ApplicationUser>().HasData(
           new ApplicationUser { Id = Guid.Parse("8B56687E-8377-4743-AAC9-08DCF5C4B471"), FullName = "Admin", Email = "admin", Password = "$2y$10$VtkJppM0TJ1d/fTye4yJWOTe22rx6Fuyf.hDlz7bbw2q9sHkPRqF2", Status = UserStatus.Active, RoleCodeId = 1, IsVerified = true, PhoneNumber = "0123456789", CreationDate = DateTime.Now, IsDeleted = false },
           new ApplicationUser { Id = Guid.Parse("8B56687E-8377-4743-AAC9-08DCF5C4B47F"), FullName = "User", Email = "user", Password = "$2a$11$ZWjOEkgvfYFnpSK.M/LEjerhgFMk4CAKR8J2cLnG6BrFN61EN/s3G", Status = UserStatus.Active, RoleCodeId = 2, IsVerified = true, PhoneNumber = "0123456789", CreationDate = DateTime.Now, IsDeleted = false },
           new ApplicationUser { Id = Guid.Parse("8B56687E-8377-4743-AAC9-08DCF5C4B470"), FullName = "Shop", Email = "shop", Password = "$2y$10$VtkJppM0TJ1d/fTye4yJWOTe22rx6Fuyf.hDlz7bbw2q9sHkPRqF2", Status = UserStatus.Active, RoleCodeId = 3, IsVerified = true, PhoneNumber = "0123456789", CreationDate = DateTime.Now, IsDeleted = false }
           );
        modelBuilder.Entity<Service>()
           .HasOne(c => c.CreatedByUser)
           .WithMany(u => u.ServiceCreated)
           .HasForeignKey(c => c.CreatedByUserId)
           .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Service>()
            .Property(s => s.Type)
            .HasConversion(v => v.ToString(), v => (ServiceType)Enum.Parse(typeof(ServiceType), v)
            );
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

        //Booking
        modelBuilder.Entity<Booking>(e =>
        {
            e.ToTable("Booking");

            // Thiết lập quan hệ với ApplicationUser và Service
            e.HasOne(b => b.User)
                .WithMany(u => u.Booking)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(b => b.Service)
                .WithMany(b => b.Booking)
                .HasForeignKey(b => b.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        //Payment
        modelBuilder.Entity<Payment>(e =>
        {
            e.ToTable("Payment");

            // Thiết lập quan hệ với ApplicationUser
            e.HasOne(p => p.ApplicationUser)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Thiết lập quan hệ với Booking
            e.HasOne(p => p.Booking)
                .WithMany(b => b.Payments)
                .HasForeignKey(p => p.BookingId)
                .OnDelete(DeleteBehavior.NoAction); // Sử dụng Cascade nếu muốn xóa Payment khi Booking bị xóa
        });
        // ShopData
        // User and InstructorData relationship
        modelBuilder.Entity<ShopData>()
            .HasOne(i => i.User)
            .WithOne(u => u.ShopData)
            .HasForeignKey<ShopData>(i => i.UserId);

        modelBuilder.Entity<ShopData>()
            .Property(s => s.CardProvider)
            .HasConversion(v => v.ToString(), v => (CardProviderEnum)Enum.Parse(typeof(CardProviderEnum), v));

        //User
        modelBuilder.Entity<ApplicationUser>()
        .Property(u => u.Status)
        .HasConversion(
            v => v.ToString(),            // Convert enum to string when saving to DB
            v => (UserStatus)Enum.Parse(typeof(UserStatus), v) // Convert string to enum when reading from DB
        );
    }
}

