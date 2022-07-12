using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Database
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                 .HasKey(e => new { e.UserID, e.RoleID });

            modelBuilder.Entity<UserRole>()
              .HasOne(ur => ur.User)
            .WithMany(u => u.UserRole)
            .HasForeignKey(ur => ur.UserID);

            modelBuilder.Entity<UserRole>()
           .HasOne(ur => ur.Role)
         .WithMany(r => r.UserRoles)
         .HasForeignKey(ur => ur.RoleID);
        }
    }
}
