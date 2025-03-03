namespace SocialMediaAPI.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;

        // Navigation Props
        public User User { get; set; }
        public Post Post{ get; set; }
    }
}
