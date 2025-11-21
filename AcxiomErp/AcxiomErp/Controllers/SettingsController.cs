using Microsoft.AspNetCore.Mvc;
using AcxiomErp.Data;
using AcxiomErp.Models;
using System.Linq;

namespace AcxiomErp.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =============================
        // 1️⃣ INDEX - SHOW ALL SETTINGS
        // =============================
        public IActionResult Index()
        {
            var settings = _context.Settings
                .OrderBy(s => s.Category)
                .ThenBy(s => s.Key)
                .ToList();

            return View(settings);
        }


        // =============================
        // 2️⃣ CREATE - GET
        // =============================
        public IActionResult Create()
        {
            return View(new Setting());
        }

        // =============================
        // 3️⃣ CREATE - POST
        // =============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Setting model)
        {
            if (ModelState.IsValid)
            {
                _context.Settings.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // =============================
        // 4️⃣ EDIT - GET
        // =============================
        public IActionResult Edit(int id)
        {
            var setting = _context.Settings.Find(id);

            if (setting == null)
                return NotFound();

            return View(setting);
        }

        // =============================
        // 5️⃣ EDIT - POST
        // =============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Setting model)
        {
            if (ModelState.IsValid)
            {
                _context.Settings.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // =============================
        // 6️⃣ DELETE
        // =============================
        public IActionResult Delete(int id)
        {
            var setting = _context.Settings.Find(id);

            if (setting == null)
                return NotFound();

            _context.Settings.Remove(setting);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // =====================================================
        // 7️⃣ OPTIONAL → ADD DEFAULT ERP SETTINGS AUTOMATICALLY
        // =====================================================
        public IActionResult LoadDefault()
        {
            if (!_context.Settings.Any())
            {
                var defaultSettings = new[]
                {
                    new Setting { Key = "CompanyName", Value = "Acxiom ERP", Category = "Application" },
                    new Setting { Key = "SystemEmail", Value = "admin@acxiom.com", Category = "Application" },
                    new Setting { Key = "DefaultCurrency", Value = "INR", Category = "Application" },

                    new Setting { Key = "AllowRegistration", Value = "true", Category = "Security" },
                    new Setting { Key = "Enable2FA", Value = "false", Category = "Security" },

                    new Setting { Key = "EnableCRM", Value = "true", Category = "Modules" },
                    new Setting { Key = "EnableHR", Value = "true", Category = "Modules" },
                    new Setting { Key = "EnableFinance", Value = "true", Category = "Modules" }
                };

                _context.Settings.AddRange(defaultSettings);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
