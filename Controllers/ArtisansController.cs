using Hareef.Data;
using Hareef.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hareef.Controllers
{
    public class ArtisansController : Controller
    {
        private readonly HareefDbContext _context;

        public ArtisansController(HareefDbContext context)
        {
            _context = context;
        }

        // عرض قائمة الحرفيين والبطاقات
        public async Task<IActionResult> Index()
        {
            var artisans = await _context.Artisans.ToListAsync();
            return View(artisans);
        }

        // حذف البطاقة (خاص بالأدمن فقط)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            // جلب إيميل المستخدم الحالي من الجلسة
            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login", "Account");
            }

            // التحقق هل المستخدم أدمن من جدول Users
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null || !user.IsAdmin)
            {
                return Unauthorized("عذراً، هذه الصلاحية خاصة بالأدمن فقط.");
            }

            // تنفيذ الحذف إذا كان المستخدم هو الأدمن
            var artisan = await _context.Artisans.FindAsync(id);
            if (artisan != null)
            {
                _context.Artisans.Remove(artisan);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}