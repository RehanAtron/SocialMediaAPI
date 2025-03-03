using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaAPI.Data;
using SocialMediaAPI.DTOs;
using SocialMediaAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CommentsController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetComments()
    {
        var comments = await _context.Comments.Include(c => c.User).Include(c => c.Post).ToListAsync();
        return Ok(_mapper.Map<IEnumerable<CommentDto>>(comments));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDto>> GetComment(int id)
    {
        var comment = await _context.Comments.Include(c => c.User).Include(c => c.Post).FirstOrDefaultAsync(c => c.Id == id);
        if (comment == null) return NotFound();
        return Ok(_mapper.Map<CommentDto>(comment));
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> CreateComment(CommentDto createCommentDto)
    {
        var comment = _mapper.Map<Comment>(createCommentDto);
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, _mapper.Map<CommentDto>(comment));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null) return NotFound();
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
