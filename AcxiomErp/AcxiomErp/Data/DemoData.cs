using AcxiomErp.Models;
using Microsoft.AspNetCore.Identity;

namespace AcxiomErp.Data;

public static class DemoData
{
    public static void Initialize(ApplicationDbContext db)
    {
        db.Database.EnsureCreated();
        if (db.Users.Any()) return;
        var user = new User { FullName = "Demo Administrator", Email = "admin@acxiom.example", Username = "demo.admin", Role = "Admin" };
        user.Password = new PasswordHasher<User>().HashPassword(user, "Demo@Erp2026!");
        db.Users.Add(user);
        db.Customers.AddRange(
            new Customer { Name = "Lotus Supplies - Demo", Email = "lotus@example.com", Phone = "0000000001", Address = "Fictional Business Park, Varanasi" },
            new Customer { Name = "Riverbank Traders - Demo", Email = "riverbank@example.com", Phone = "0000000002", Address = "Fictional Market Road, Lucknow" },
            new Customer { Name = "North Star Books - Demo", Email = "books@example.com", Phone = "0000000003", Address = "Fictional College Road, Delhi" });
        db.Employees.AddRange(
            new Employee { Name = "Aarav Sharma - Demo", Email = "aarav@example.com", Phone = "0000000011", Designation = "ERP Administrator" },
            new Employee { Name = "Meera Verma - Demo", Email = "meera@example.com", Phone = "0000000012", Designation = "Accounts Executive" },
            new Employee { Name = "Rohan Gupta - Demo", Email = "rohan@example.com", Phone = "0000000013", Designation = "Customer Support" });
        db.Reports.Add(new Report { Title = "Monthly operations review - Demo", Description = "Fictional customers and employees prepared for an interactive project demonstration.", CreatedDate = new DateTime(2026, 11, 1) });
        db.Settings.AddRange(
            new Setting { Key = "CompanyName", Value = "Acxiom ERP - Interview Demo", Category = "Application" },
            new Setting { Key = "DefaultCurrency", Value = "INR", Category = "Application" },
            new Setting { Key = "DemoData", Value = "Fictional records; changes reset on host restart", Category = "Application" });
        db.SaveChanges();
    }
}
