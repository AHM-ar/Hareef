using Hareef.Models;
using Microsoft.EntityFrameworkCore;
namespace Hareef.Data
{
    public class HareefDbContext : DbContext
    {
        public HareefDbContext(DbContextOptions<HareefDbContext> options) : base(options)
        {
        }

            public DbSet<Artisan> Artisans { get; set; }
        }
    }

