using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class UsersController : BaseApiController
{
    private readonly DataContext _context;
    public UsersController(DataContext context)
    {
        _context = context;
    }


   [HttpGet]
   [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<AppUser>>>GetUsers()
    {
        return await _context.users.ToListAsync();
    }


   [HttpGet("{id}")]
   [Authorize]
    public async Task<ActionResult<AppUser>>GetUser(int id)
    {
        var currentUser = System.Security.Claims.ClaimTypes.NameIdentifier;
        Console.WriteLine(currentUser);
        return await _context.users.FindAsync(id);
    }
}