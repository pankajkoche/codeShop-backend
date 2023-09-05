using CodeshopApi.Models.Blog;
using CodeshopApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeshopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetBlogPosts()
        {
            try
            {
                var blogPosts = await _blogPostRepository.GetAllAsync();
                return Ok(blogPosts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost>> GetBlogPost(int id)
        {
            try
            {
                var blogPost = await _blogPostRepository.GetByIdAsync(id);
                if (blogPost == null)
                {
                    return NotFound($"Blog post with ID {id} not found");
                }
                return Ok(blogPost);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<BlogPost>> CreateBlogPost([FromBody] BlogPostRequest blogPostRequest)
        {
            try
            {
                if (blogPostRequest == null)
                {
                    return BadRequest("Blog post object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var blogPost = new BlogPost
                {
                    Title = blogPostRequest.Title,
                    Contents = blogPostRequest.Sections.Select(section => new BlogContent
                    {
                        Type = section.Type,
                        Content = section.Content
                    }).ToList()
                };

                await _blogPostRepository.CreateAsync(blogPost);

                return CreatedAtAction(nameof(GetBlogPost), new { id = blogPost.Id }, blogPost);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBlogPost(int id, [FromBody] BlogPostRequest updatedBlogPostRequest)
        {
            try
            {
                if (updatedBlogPostRequest == null)
                {
                    return BadRequest("Blog post object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var existingBlogPost = await _blogPostRepository.GetByIdAsync(id);
                if (existingBlogPost == null)
                {
                    return NotFound($"Blog post with ID {id} not found");
                }

                existingBlogPost.Title = updatedBlogPostRequest.Title;
                existingBlogPost.Contents = updatedBlogPostRequest.Sections.Select(section => new BlogContent
                {
                    Type = section.Type,
                    Content = section.Content
                }).ToList();

                await _blogPostRepository.UpdateAsync(existingBlogPost);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBlogPost(int id)
        {
            try
            {
                var existingBlogPost = await _blogPostRepository.GetByIdAsync(id);
                if (existingBlogPost == null)
                {
                    return NotFound($"Blog post with ID {id} not found");
                }

                await _blogPostRepository.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
