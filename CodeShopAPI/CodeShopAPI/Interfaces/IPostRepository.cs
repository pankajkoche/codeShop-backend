using CodeShopAPI.Dto;

namespace CodeShopAPI.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<PostDto>> GetAllPostsAsync();
        Task<PostDto> GetPostByIdAsync(int id);
        Task<IEnumerable<PostDto>> GetPostsByCategoryIdAsync(int categoryId);
        Task CreatePostAsync(PostDto postDto);
        Task UpdatePostAsync(int id, PostDto postDto);
        Task DeletePostAsync(int id);
    }
}
