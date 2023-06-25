using CodeShopAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeShopAPI.Data
{

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Post>()
                .Property(p => p.Title)
                .IsRequired();

            modelBuilder.Entity<Post>()
                .Property(p => p.CategoryId)
                .IsRequired();

            modelBuilder.Entity<Post>()
                .Property(p => p.SrNo)
                .IsRequired();

            modelBuilder.Entity<Post>()
                .Property(p => p.Content)
                .IsRequired();
        }
    }
}

