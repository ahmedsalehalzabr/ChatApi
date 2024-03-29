﻿using ChatApi.Data;
using ChatApi.DTOs;
using ChatApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<AppUser>> Regsiter([FromBody] RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUser>> Login(LoginDto loginDto)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => 
            x.UserName == loginDto.Username);
            if (user == null) return Unauthorized("invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
                    {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("invalid password");
            }
            return user;
        }

        private async Task<bool> UserExists(string username)
        {
            return await _db.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

    }
}
