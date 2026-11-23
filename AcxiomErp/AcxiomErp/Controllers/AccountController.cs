using Microsoft.AspNetCore.Mvc;
using AcxiomErp.Models;
using AcxiomErp.Data;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace AcxiomErp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context) => _context = context;
        public IActionResult Index() => View(nameof(AcxiomHome));
        public IActionResult AcxiomHome()
        {
            return View();
        }
        public IActionResult Signup() => View();

        [HttpPost]
        public IActionResult Signup(User model)
        {
            if (ModelState.IsValid)
            {
                model.Role = "Employee";
                model.Password = new PasswordHasher<User>().HashPassword(model, model.Password);
                _context.Users.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            var valid = false;
            if (user != null && !string.IsNullOrEmpty(password))
            {
                var hasher = new PasswordHasher<User>();
                try { valid = hasher.VerifyHashedPassword(user, user.Password, password) != PasswordVerificationResult.Failed; }
                catch (FormatException) { valid = false; }
                if (!valid && user.Password == password)
                {
                    valid = true;
                    user.Password = hasher.HashPassword(user, password);
                    _context.SaveChanges();
                }
            }
            if (valid && user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);
                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.Error = "Invalid Username or Password";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Demo([FromServices] IConfiguration configuration)
        {
            if (!configuration.GetValue<bool>("DEMO_MODE")) return NotFound();
            HttpContext.Session.SetString("Username", "demo.admin");
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
