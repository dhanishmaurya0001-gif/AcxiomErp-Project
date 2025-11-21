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
    }
}
