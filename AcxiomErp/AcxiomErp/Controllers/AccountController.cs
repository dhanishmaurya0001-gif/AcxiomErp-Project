using Microsoft.AspNetCore.Mvc;
using AcxiomErp.Models;
using AcxiomErp.Data;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace AcxiomErp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context) => _context = context;
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
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
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
    }
}
