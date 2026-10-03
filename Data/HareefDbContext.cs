using Microsoft.EntityFrameworkCore;
using Hareef.Models;

namespace Hareef.Data
{
    public class HareefDbContext : DbContext
    {
        public HareefDbContext(DbContextOptions<HareefDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Artisan> Artisans { get; set; }
    }
}