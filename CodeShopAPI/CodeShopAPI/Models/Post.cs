namespace CodeShopAPI.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public int SrNo { get; set; }
        public string Content { get; set; }
    }
}
