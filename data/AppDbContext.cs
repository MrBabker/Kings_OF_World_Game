using king.Models.players;
using Microsoft.EntityFrameworkCore;

namespace king.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
         : base(options)
        {
        }


        public DbSet<PlayerModel> Players { get; set; }
    }
}
