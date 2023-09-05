namespace CodeshopApi.Models.Blog
{
    public class BlogContent
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }
    }
}
