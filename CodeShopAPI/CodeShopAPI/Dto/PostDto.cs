namespace CodeShopAPI.Dto
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public int SrNo { get; set; }
        public string Content { get; set; }
    }
}
