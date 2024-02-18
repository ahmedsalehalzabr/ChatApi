using ChatApi.Data;
using ChatApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace ChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AccountController(AppDbContext db) 
        {
            _db = db;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Regsiter(string username, string password)
        {
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return Ok(user);
        }
    }
}
