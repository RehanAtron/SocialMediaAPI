using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaAPI.Data;
using SocialMediaAPI.DTOs;
using SocialMediaAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public PostsController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetPosts()
    {
        var posts = await _context.Posts.Include(p => p.User).ToListAsync();
        return Ok(_mapper.Map<IEnumerable<PostDto>>(posts));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto>> GetPost(int id)
    {
        var post = await _context.Posts.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);
        if (post == null) return NotFound();
        return Ok(_mapper.Map<PostDto>(post));
    }

    [HttpPost]
    public async Task<ActionResult<PostDto>> CreatePost(PostDto createPost)
    {
        var post = _mapper.Map<Post>(createPost);
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPost), new { id = post.Id }, _mapper.Map<PostDto>(post));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, Post postDto)
    {
        if (id != postDto.Id) return BadRequest();
        var post = await _context.Posts.FindAsync(id);
        if (post == null) return NotFound();

        _mapper.Map(postDto, post);
        _context.Entry(post).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null) return NotFound();
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
