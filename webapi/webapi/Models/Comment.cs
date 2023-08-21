namespace webapi.Models
{
    public class Comment
    {
        public Comment(int id, int blogId, string content, string author, int parentId)
        {
            Id = id;
            BlogId = blogId;
            Content = content;
            Author = author;
            ParentId = parentId;
        }

        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public int ParentId { get; set; } = -1;
    }
}
