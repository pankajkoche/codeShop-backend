using CodeShopAPI.Data;
using CodeShopAPI.Dto;
using CodeShopAPI.Interfaces;
using CodeShopAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeShopAPI.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;

        public PostRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
        {
            // Use AutoMapper to map the data model to DTO
            return await _context.Posts
                .Select(p => new PostDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    CategoryId = p.CategoryId,
                    SrNo = p.SrNo,
                    Content = p.Content
                })
                .ToListAsync();
        }

        public async Task<PostDto> GetPostByIdAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
                return null;

            // Use AutoMapper to map the data model to DTO
            return new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                CategoryId = post.CategoryId,
                SrNo = post.SrNo,
                Content = post.Content
            };
        }

        public async Task CreatePostAsync(PostDto postDto)
        {
            // Use AutoMapper to map the DTO to data model
            var post = new Post
            {
                Title = postDto.Title,
                CategoryId = postDto.CategoryId,
                SrNo = postDto.SrNo,
                Content = postDto.Content
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }
       public async Task<IEnumerable<PostDto>> GetPostsByCategoryIdAsync(int categoryId)
        {
            var posts = await _context.Posts.Where(p => p.CategoryId == categoryId).ToListAsync();
            return posts.Select(MapToDto);
        }

        public async Task UpdatePostAsync(int id, PostDto postDto)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
                return;

            // Update the data model
            post.Title = postDto.Title;
            post.CategoryId = postDto.CategoryId;
            post.SrNo = postDto.SrNo;
            post.Content = postDto.Content;

            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
                return;

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }
        private static PostDto MapToDto(Post post)
        {
            return new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                CategoryId = post.CategoryId,
                SrNo = post.SrNo,
                Content = post.Content
            };
        }
    }

 }
