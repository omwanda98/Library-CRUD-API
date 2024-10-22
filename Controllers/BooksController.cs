using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Models;
using LibraryAPI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly LibraryAPIContext _context;

    public BooksController(LibraryAPIContext context)
    {
        _context = context;
    }

    // GET: api/Books (Accessible by both Admin and User)
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        return await _context.Book.ToListAsync();
    }

    // GET: api/Books/5 (Accessible by both Admin and User)
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await _context.Book.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        return book;
    }

    // POST: api/Books (Accessible only by Admin)
    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult<Book>> PostBook(Book book)
    {
        _context.Book.Add(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBook), new { id = book.id }, book);
    }

    // PUT: api/Books/5 (Accessible only by Admin)
    [HttpPut("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> PutBook(int id, Book book)
    {
        if (id != book.id)
        {
            return BadRequest();
        }

        _context.Entry(book).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Books/5 (Accessible only by Admin)
    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Book.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        _context.Book.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
