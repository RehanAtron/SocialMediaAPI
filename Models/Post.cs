namespace SocialMediaAPI.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Props
        public User User { get; set; }
        public List<Comment>? Comments { get; set; } = new();
        public List<Like>? Likes { get; set; } = new();
    }
}
