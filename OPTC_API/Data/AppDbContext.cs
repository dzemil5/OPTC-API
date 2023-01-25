using Microsoft.EntityFrameworkCore;
using OPTC_API.Models;

namespace OPTC_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Character> Characters { get; set; }
    }
}
