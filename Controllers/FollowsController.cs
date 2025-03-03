using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaAPI.Data;
using SocialMediaAPI.Models;
using SocialMediaAPI.DTOs;

[Route("api/[controller]")]
[ApiController]
public class FollowsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public FollowsController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FollowDto>>> GetFollows()
    {
        var follows = await _context.Follows.Include(f => f.Follower).Include(f => f.Following).ToListAsync();
        return Ok(_mapper.Map<IEnumerable<FollowDto>>(follows));
    }

    [HttpPost]
    public async Task<ActionResult<FollowDto>> FollowUser(FollowDto followDto)
    {
        var follow = _mapper.Map<Follow>(followDto);
        _context.Follows.Add(follow);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetFollows), new { id = follow.Id }, _mapper.Map<FollowDto>(follow));
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
