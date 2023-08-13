namespace webapi.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int? AuthorId { get; set; }
        public int? ParentId { get; set; }
    }
}
