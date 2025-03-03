using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaAPI.Data;
using SocialMediaAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class FollowsController : ControllerBase
{
    private readonly AppDbContext _context;

    public FollowsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Follow>>> GetFollows()
    {
        return await _context.Follows.Include(f => f.Follower).Include(f => f.Following).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Follow>> FollowUser(Follow follow)
    {
        _context.Follows.Add(follow);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetFollows), new { id = follow.Id }, follow);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> UnfollowUser(int id)
    {
        var follow = await _context.Follows.FindAsync(id);
        if (follow == null) return NotFound();
        _context.Follows.Remove(follow);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
