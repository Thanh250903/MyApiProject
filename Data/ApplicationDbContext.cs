using Microsoft.EntityFrameworkCore;
using MyApiProject.Models;

namespace MyApiProject.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "john_doe", Password = "password123" },
                new User { Id = 2, Username = "jane_doe", Password = "password456" }
            );

            modelBuilder.Entity<Address>().HasData(
                new Address { Id = 1, Name = "123 Main St", UserId = 1 },
                new Address { Id = 2, Name = "456 Oak St", UserId = 2 }
            );
        }
    }
}
