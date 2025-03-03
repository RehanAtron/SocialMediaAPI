namespace SocialMediaAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfilePic { get; set; }
        public string Bio { get; set; }

        // Navigation properties
        public List<Post>? Posts { get; set; } = new();
        public List<Comment>? Comments { get; set; } = new();
        public List<Like>? Likes { get; set; } = new();
        public List<Follow>? Followers { get; set; } = new();
        public List<Follow>? Following { get; set; } = new();

    }
}
