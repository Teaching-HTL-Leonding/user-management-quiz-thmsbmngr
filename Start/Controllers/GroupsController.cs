using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;

namespace UserManagement.Controllers
{
    public record GroupResult(int Id, string Name);

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "administrator")]
    public class GroupsController : ControllerBase
    {
        private readonly UserManagementDataContext _context;

        public GroupsController(UserManagementDataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GroupResult>> GetById([FromRoute] int id)
        {
            var group = await _context.Groups
                .Where(g => g.Id == id)
                .Select(g => new GroupResult(g.Id, g.Name))
                .FirstOrDefaultAsync();

            if (group == null)
                return NotFound();
                
            return Ok(group);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetAll()
        {
            return Ok(await _context.Groups
                .Select(g => new GroupResult(g.Id, g.Name))
                .ToListAsync());
        }
    }
}