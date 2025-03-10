﻿namespace SocialMediaAPI.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;

        // Navigate Props
        public User User { get; set; }
        public Post Post { get; set; }
    }
}
