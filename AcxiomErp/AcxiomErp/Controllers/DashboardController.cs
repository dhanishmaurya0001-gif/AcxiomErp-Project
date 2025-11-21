using AcxiomErp.Data;
using AcxiomErp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AcxiomErp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string search)
        {
            // Initialize ViewModel
            var model = new ReportsDashboardViewModel
            {
                TotalCustomers = _context.Customers.Count(),
                TotalEmployees = _context.Employees.Count(),
                TotalReports = _context.Reports.Count()
            };

            if (string.IsNullOrEmpty(search))
            {
                // Default: show first 5 entries for dashboard summary
                model.Customers = _context.Customers.Take(5).ToList();
                model.Employees = _context.Employees.Take(5).ToList();
                model.Reports = _context.Reports.Take(5).ToList();
            }
            else
            {
                string lower = search.ToLower();

                // 🔹 Search Customers by Name, Email, or Phone
                model.Customers = _context.Customers
                    .Where(c => c.Name.ToLower().Contains(lower) ||
                                c.Email.ToLower().Contains(lower) ||
                                c.Phone.ToLower().Contains(lower))
                    .ToList();

                // 🔹 Search Employees by Name, Email, or Designation
                model.Employees = _context.Employees
                    .Where(e => e.Name.ToLower().Contains(lower) ||
                                e.Email.ToLower().Contains(lower) ||
                                e.Designation.ToLower().Contains(lower))
                    .ToList();

                // 🔹 Search Reports by Title or Description
                model.Reports = _context.Reports
                    .Where(r => r.Title.ToLower().Contains(lower) ||
                                r.Description.ToLower().Contains(lower))
                    .ToList();
            }

            return View(model);
        }
    }
}
