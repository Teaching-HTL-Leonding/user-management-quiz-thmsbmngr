using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;

namespace UserManagement.Controllers
{
    public record UserResult(int Id, string NameIdentifier, string Email, string? FirstName, string? LastName);
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserManagementDataContext _context;

        public UsersController(UserManagementDataContext context)
        {
            _context = context;
        }
        
        [HttpGet("me")]
        public async Task<UserResult> Me()
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var user = await _context.Users
                .Where(u => u.NameIdentifier == userId)
                .Select(u => new UserResult(u.Id, u.NameIdentifier, u.Email, u.FirstName, u.LastName))
                .FirstAsync();

            return user;
        }

        [HttpGet]
        public async Task<IEnumerable<UserResult>> GetAll()
        {
            return await _context.Users
                .Select(u => new UserResult(u.Id, u.NameIdentifier, u.Email, u.FirstName, u.LastName))
                .ToListAsync();
        }
        
        
    }
}