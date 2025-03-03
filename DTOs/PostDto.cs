namespace SocialMediaAPI.DTOs
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public List<CommentDto>? Comments { get; set; }
        public List<LikeDto>? Likes { get; set; }
    }
}
