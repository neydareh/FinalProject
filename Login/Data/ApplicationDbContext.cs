using Login.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace Login.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public override int SaveChanges()
        {
            var entries = ChangeTracker  
                .Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified)); 
            

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;
            }

            return base.SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("dbo");

            builder.Entity<ApplicationUser>(option => option.ToTable(name: "User"));
            builder.Entity<IdentityRole>(option => option.ToTable(name: "Role"));
            builder.Entity<IdentityUserRole<string>>(option => option.ToTable(name: "UserRoles"));
            builder.Entity<IdentityUserClaim<string>>(option => option.ToTable(name: "UserClaims"));
            builder.Entity<IdentityUserLogin<string>>(option => option.ToTable(name: "UserLogins"));
            builder.Entity<IdentityRoleClaim<string>>(option => option.ToTable(name: "RoleClaims"));
            builder.Entity<IdentityUserToken<string>>(option => option.ToTable(name: "UserTokens"));
        }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Comment> Comments { get; set; }
        
    }
}
