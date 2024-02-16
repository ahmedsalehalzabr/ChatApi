using ChatApi.Data;
using ChatApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public UsersController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]

        public async Task< ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await _db.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task< ActionResult<AppUser>> GetUserById(int id)
        {
            return await _db.Users.FindAsync(id);
        }
    }
}
