using Hareef.Data;
using Hareef.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Hareef.Controllers
{
    public class HomeController : Controller
    {
        private readonly HareefDbContext _context;

        public HomeController(HareefDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // صفحة الداشبورد التي تظهر بعد تسجيل الدخول مباشرة
        public async Task<IActionResult> Dashboard(string searchQuery)
        {
            // التحقق مما إذا كان المستخدم مسجل دخوله عبر الـ Session
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login", "Account");
            }

            // جلب البطاقات مع دعم البحث عن مهنة أو اسم
            var query = _context.Artisans.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                // تم تصحيح Where بحرف W كبير لتجنب الخطأ البرمجي
                query = query.Where(a => a.Profession.Contains(searchQuery) || a.Name.Contains(searchQuery));
            }

            var artisansList = await query.ToListAsync();

            // تمرير نتائج البحث والبيانات للـ View
            ViewData["SearchQuery"] = searchQuery;
            ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            ViewData["IsAdmin"] = HttpContext.Session.GetString("IsAdmin");

            return View(artisansList);
        }
    }
}