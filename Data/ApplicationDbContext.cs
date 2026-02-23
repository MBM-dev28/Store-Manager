using Microsoft.EntityFrameworkCore;
using Store_Manager.Data.Entities;

namespace Store_Manager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Chain> Chains { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Chain>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Store>()
                .HasIndex(s => s.Number)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
