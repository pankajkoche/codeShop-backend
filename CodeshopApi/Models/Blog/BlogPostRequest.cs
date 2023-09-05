namespace CodeshopApi.Models.Blog
{
    public class BlogPostRequest
    {
        public string Title { get; set; }
        public List<BlogPostSection> Sections { get; set; }
    }

    public class BlogPostSection
    {
        public string Type { get; set; }
        public string Content { get; set; }
    }
}
