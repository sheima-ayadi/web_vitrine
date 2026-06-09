using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InfiniSoft.Admin.Data;
using InfiniSoft.Admin.Models;

namespace InfiniSoft.Admin.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetPosts()
        {
            return await _context.BlogPosts.OrderByDescending(p => p.Id).ToListAsync();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<BlogPost>> CreatePost(BlogPost post)
        {
            _context.BlogPosts.Add(post);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPosts), new { id = post.Id }, post);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, BlogPost post)
        {
            if (id != post.Id) return BadRequest();

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.BlogPosts.FindAsync(id);
            if (post == null) return NotFound();

            _context.BlogPosts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostExists(int id)
        {
            return _context.BlogPosts.Any(e => e.Id == id);
        }
    }
