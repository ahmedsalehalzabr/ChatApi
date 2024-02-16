using ChatApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApi.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions option) : base(option)
        {
            
        }

        public DbSet<AppUser> Users { get; set; }

    }
}
