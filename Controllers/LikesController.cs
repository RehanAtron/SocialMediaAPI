using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaAPI.Data;
using SocialMediaAPI.DTOs;
using SocialMediaAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class LikesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public LikesController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LikeDto>>> GetLikes()
    {
        var likes = await _context.Likes.Include(l => l.User).Include(l => l.Post).ToListAsync();
        return Ok (_mapper.Map<IEnumerable<LikeDto>>(likes));
    }

    [HttpPost]
    public async Task<ActionResult<LikeDto>> LikePost(LikeDto likeDto)
    {
        var existingLike = await _context.Likes
        .FirstOrDefaultAsync(l => l.UserId == likeDto.UserId && l.PostId == likeDto.PostId);

        if (existingLike != null)
        {
            return BadRequest("User has already liked this post.");
        }

        var like = _mapper.Map<Like>(likeDto);
        _context.Likes.Add(like);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLikes), new { id = like.Id }, _mapper.Map<LikeDto>(like));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> UnlikePost(int id)
    {
        var like = await _context.Likes.FindAsync(id);
        if (like == null) return NotFound();
        _context.Likes.Remove(like);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
