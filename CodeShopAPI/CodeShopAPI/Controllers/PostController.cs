using CodeShopAPI.Dto;
using CodeShopAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeShopAPI.Controllers
{
    // PostController.cs

    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _repository;

        public PostController(IPostRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _repository.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _repository.GetPostByIdAsync(id);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostDto postDto)
        {
            await _repository.CreatePostAsync(postDto);
            return Ok();
        }
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetPostsByCategorySortedBySrNo(int categoryId)
        {
            var posts = await _repository.GetPostsByCategoryIdAsync(categoryId);
            if (posts == null || !posts.Any())
            {
                return NotFound();
            }

            var sortedPosts = posts.OrderBy(p => p.SrNo);

            return Ok(sortedPosts);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, PostDto postDto)
        {
            await _repository.UpdatePostAsync(id, postDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _repository.DeletePostAsync(id);
            return Ok();
        }
    }

}
