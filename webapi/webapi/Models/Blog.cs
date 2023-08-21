namespace webapi.Models
{
    public class Blog
    {
        public Blog(int id, int userId, string title, string content, string createTime)
        {
            Id = id;
            UserId = userId;
            Title = title;
            Content = content;
            CreateTime = createTime;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CreateTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
    }
}
