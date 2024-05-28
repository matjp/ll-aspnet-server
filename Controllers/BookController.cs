using AspNetServer.Models;
using AspNetServer.Services;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace AspNetServer.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private BookService _bookService;

    public BooksController(MySqlConnection connection)
    {
        _bookService = new BookService(connection);
    }

    [HttpGet("{borrowed}")]
    public async Task<ActionResult<List<Book>>> Get(int borrowed) =>
        await _bookService.Get(borrowed);

    [HttpPut("borrow/{title}")]
    public async Task<IActionResult> Borrow(string title)
    {  
        await _bookService.Borrow(title);
        return NoContent();        
    }

    [HttpPut("return/{title}")]
    public async Task<IActionResult> Return(string title)
    {  
        await _bookService.Return(title);
        return NoContent();        
    }
 }