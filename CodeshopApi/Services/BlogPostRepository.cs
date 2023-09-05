using CodeshopApi.Data;
using CodeshopApi.Models.Blog;
using CodeshopApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeshopApi.Services
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogApplicationDbContext _context;

        public BlogPostRepository(BlogApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _context.BlogPosts.Include(bp => bp.Contents).ToListAsync();
        }

        public async Task<BlogPost> GetByIdAsync(int id)
        {
            return await _context.BlogPosts.Include(bp => bp.Contents).FirstOrDefaultAsync(bp => bp.Id == id);
        }

        public async Task CreateAsync(BlogPost blogPost)
        {
            if (blogPost == null)
            {
                throw new ArgumentNullException(nameof(blogPost));
            }

            await _context.BlogPosts.AddAsync(blogPost);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BlogPost blogPost)
        {
            _context.Entry(blogPost).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                throw new ArgumentNullException(nameof(blogPost));
            }

            _context.BlogPosts.Remove(blogPost);
            await _context.SaveChangesAsync();
        }
    }
}

