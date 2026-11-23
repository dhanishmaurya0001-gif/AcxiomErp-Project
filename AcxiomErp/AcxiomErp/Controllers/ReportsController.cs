using AcxiomErp.Data;
using AcxiomErp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AcxiomErp.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var vm = new ReportsDashboardViewModel
            {
                TotalCustomers = _context.Customers.Count(),
                TotalEmployees = _context.Employees.Count(),
                TotalReports = _context.Reports.Count(),

                Customers = _context.Customers.Take(5).ToList(),
                Employees = _context.Employees.Take(5).ToList(),
                Reports = _context.Reports.Take(5).ToList()
            };

            return View(vm);
        }
        public IActionResult Create() => View(new Report { CreatedDate = DateTime.Today });

        [HttpPost]
        public IActionResult Create(Report model)
        {
            if (!ModelState.IsValid) return View(model);
            model.Id = 0;
            if (model.CreatedDate == default) model.CreatedDate = DateTime.Today;
            _context.Reports.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var report = _context.Reports.Find(id);
            return report == null ? NotFound() : View(report);
        }

        [HttpPost]
        public IActionResult Edit(Report model)
        {
            if (!ModelState.IsValid) return View(model);
            var report = _context.Reports.Find(model.Id);
            if (report == null) return NotFound();
            report.Title = model.Title;
            report.Description = model.Description;
            report.CreatedDate = model.CreatedDate;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var report = _context.Reports.Find(id);
            if (report == null) return NotFound();
            _context.Reports.Remove(report);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
