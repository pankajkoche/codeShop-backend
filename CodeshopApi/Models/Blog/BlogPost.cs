namespace CodeshopApi.Models.Blog
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SubjectId { get; set; }
        public ICollection<BlogContent> Contents { get; set; }
    }
}
