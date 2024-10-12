using Server.Application.Enum;
using Server.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain
{
    public static class DbInitializer
    {
        public static async Task Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.Users.Any())
            {
                return;
            }

            // Create admin users
            var admin = new ApplicationUser
            {
                FullName = "admin1",
                Email = "admin1@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("AdminPassword123"),
                IsVerified = true,
                RoleCodeId = context.Role.Single(r => r.RoleName == "Admin").Id
            };
            context.Users.Add(admin);

            // Create shop user
            var shop = new ApplicationUser
            {
                FullName = "instructor1",
                Email = "kietuctse161952@fpt.edu.vn",
                Password = BCrypt.Net.BCrypt.HashPassword("Kiet@0793213702"),
                Status = UserStatus.Active,
                IsVerified = true,
                RoleCodeId = context.Role.Single(r => r.RoleName == "Shop").Id
            };
            context.Users.Add(shop);

            // Create users
            var user = new ApplicationUser
            {
                FullName = "Gia Phuc",
                Email = "ungcamtuankiet94@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("Kiet@0793213702"),
                IsVerified = true,
                RoleCodeId = context.Role.Single(r => r.RoleName == "User").Id
            };
            context.Users.Add(user);
        }
    }
}
