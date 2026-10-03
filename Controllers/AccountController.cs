using Hareef.Data;
using Hareef.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hareef.Controllers
{
    public class AccountController : Controller
    {
        private readonly HareefDbContext _context;

        public AccountController(HareefDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "البريد الإلكتروني وكلمة المرور مطلوبان.");
                return View();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && u.Password == password);

            if (user == null)
            {
                ModelState.AddModelError("", "البريد الإلكتروني أو كلمة المرور غير صحيحة!");
                return View();
            }

            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserName", user.FullName ?? user.Name ?? "مستخدم");
            HttpContext.Session.SetString("IsAdmin", user.IsAdmin.ToString());

            return RedirectToAction("Index", "Artisans");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User model, string Profession, string Bio, string WhatsAppNumber, string ProfileImage)
        {
            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.FullName))
            {
                ModelState.AddModelError("", "الرجاء إدخال البريد الإلكتروني، كلمة المرور، والاسم الكامل.");
                return View(model);
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == model.Email.ToLower());
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "البريد الإلكتروني مستخدم بالفعل.");
                return View(model);
            }

            model.Name = model.FullName;
            model.IsAdmin = false;
            _context.Users.Add(model);
            await _context.SaveChangesAsync();

            var artisan = new Artisan
            {
                Name = model.FullName,
                Email = model.Email,
                Profession = Profession ?? "مهني / حرفي",
                Bio = Bio ?? "لا توجد نبذة تعريفية.",
                WhatsAppNumber = WhatsAppNumber ?? "",
                ProfileImage = ProfileImage ?? "/images/default-profile.png"
            };

            _context.Artisans.Add(artisan);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}