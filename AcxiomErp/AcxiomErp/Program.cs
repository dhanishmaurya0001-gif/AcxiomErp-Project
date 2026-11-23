using AcxiomErp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
var demo = builder.Configuration.GetValue<bool>("DEMO_MODE");

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    if (demo)
        options.UseSqlite(builder.Configuration["DEMO_DB"] ?? "Data Source=acxiom-demo.db");
    else
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add Session for login
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var app = builder.Build();
if (demo) {
    using var scope = app.Services.CreateScope();
    DemoData.Initialize(scope.ServiceProvider.GetRequiredService<ApplicationDbContext>());
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();  // Important for login session
app.Use(async (context, next) => {
    var path = context.Request.Path;
    if ((path.StartsWithSegments("/Dashboard") || path.StartsWithSegments("/Customers") ||
         path.StartsWithSegments("/Employees") || path.StartsWithSegments("/Reports") ||
         path.StartsWithSegments("/Settings")) &&
        string.IsNullOrEmpty(context.Session.GetString("Username"))) {
        context.Response.Redirect("/Account/Login");
        return;
    }
    await next();
});
app.MapGet("/health", () => Results.Ok(new { status = "ok", demo }));
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.Run();
