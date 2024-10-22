using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Models;
using LibraryAPI.Services;
using LibraryAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly LibraryAPIContext _context;  // Database context for saving users

    private readonly JwtService _jwtService;

    public AuthController(LibraryAPIContext context, JwtService jwtService)
    {
        _context = context;        // Inject the context
        _jwtService = jwtService;
    }

    // Register a new user
    [HttpPost("register")]
    public async Task<IActionResult> Register(User user)
    {
        // Add user to the database context
        _context.User.Add(user);

        // Save the changes to the database
        await _context.SaveChangesAsync();

        return Ok(new { message = "Registration successful" });
    }

    // Login the user and generate JWT
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User loginRequest)
    {
        // Retrieve the user from the database based on username and password
        var user = await _context.User
            .SingleOrDefaultAsync(x => x.Username == loginRequest.Username && x.Password == loginRequest.Password);
        
        if (user == null)
        {
            return Unauthorized();  // Return 401 if user not found
        }

        // Generate the JWT token for the authenticated user
        var token = _jwtService.GenerateJwtToken(user);
        return Ok(new { token });
    }
}
