using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet.Actions;

namespace MusicBlog.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed roles (User, Admin, SuperAdmin)
            var adminRoleId = "cc09ffd6-2bd2-41f9-8834-15c665caaa99";
            var superAdminRoleId = "959999e4-368f-4222-8c6e-c90e283802be";
            var userRoleId = "a69467e1-a974-4843-a42f-2cf931deaba0";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name= "Admin",
                    NormalizedName = "Admin",
                    Id = "cc09ffd6-2bd2-41f9-8834-15c665caaa99",
                    ConcurrencyStamp = adminRoleId
                },

                new IdentityRole
                {
                    Name= "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },

                new IdentityRole
                {
                    Name= "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // Seed SuperAdminUser
            var superAdminId = "f42061a0-ad06-43c3-b67e-9cc8a2ffa63d";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@musicblog.com",
                Email = "superadmin@musicblog.com",
                NormalizedEmail = "superadmin@musicblog.com".ToUpper(),
                NormalizedUserName = "superadmin@musicblog.com".ToUpper(),
                Id = superAdminId,

            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "SuperAdmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // Add all roles to SuperAdminUser
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                },
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);

        }
    }



}