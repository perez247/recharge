using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using recharge.api.Core.Models;

namespace recharge.api.Persistence.Repository
{
    public class DataContext : IdentityDbContext<User, Role, Guid,
        IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public DbSet<UserTransaction> UsersTransactions { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DataContext(DbContextOptions<DataContext> options)
        : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder) {

            builder.Entity<UserRole>(userRole =>{
                userRole.HasKey(ur => new {ur.UserId, ur.RoleId});

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            // builder.Entity<User>(user => {
            //     // user.HasIndex(u => u.PhoneNumber).IsUnique();
            //     // user.HasAlternateKey(u => u.PhoneNumber);
            // });

            base.OnModelCreating(builder);
        }
    }
}