using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaAPI.Data;
using SocialMediaAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class LikesController : ControllerBase
{
    private readonly AppDbContext _context;

    public LikesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Like>>> GetLikes()
    {
        return await _context.Likes.Include(l => l.User).Include(l => l.Post).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Like>> LikePost(Like like)
    {
        _context.Likes.Add(like);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetLikes), new { id = like.Id }, like);
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
