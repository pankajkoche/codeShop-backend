using CodeshopApi.Models.Blog;
using Microsoft.EntityFrameworkCore;

namespace CodeshopApi.Data
{
    public class BlogApplicationDbContext : DbContext
    {
        public BlogApplicationDbContext(DbContextOptions<BlogApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogContent> BlogContents { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogContent>()
                .HasOne(bc => bc.BlogPost)
                .WithMany(bp => bp.Contents)
                .HasForeignKey(bc => bc.BlogPostId);
        }
    }
}
