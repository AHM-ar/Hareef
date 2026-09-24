using Microsoft.AspNetCore.Mvc;
using Hareef.Data;
namespace Hareef.Controllers
{
    public class ArtisansController : Controller
    {
        private readonly HareefDbContext _context;
        public ArtisansController(HareefDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var artisans = _context.Artisans.ToList();
            return View(artisans);
        }
    }
}
