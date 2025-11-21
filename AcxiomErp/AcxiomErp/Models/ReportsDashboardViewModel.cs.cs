using System.Collections.Generic;

namespace AcxiomErp.Models
{
    public class ReportsDashboardViewModel
    {
        public int TotalCustomers { get; set; }
        public int TotalEmployees { get; set; }
        public int TotalReports { get; set; }

        public List<Customer> Customers { get; set; } = new();
        public List<Employee> Employees { get; set; } = new();
        public List<Report> Reports { get; set; } = new();
    }
}
