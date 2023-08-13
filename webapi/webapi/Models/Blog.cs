namespace webapi.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string CreateTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
    }
}
