using LuftBornCodeTest.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace LuftBornCodeTest.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Publisher> Publishers { get; set; }
    }
}
