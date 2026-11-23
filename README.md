# Acxiom ERP - Interactive Portfolio Demo

An ASP.NET Core MVC business-management project by Dhanish Kumar Maurya. The working demonstration includes account login, a customer register, employee records, report records, a searchable dashboard, and editable settings records.

## Try the demonstration

**[Open the live Acxiom ERP demo](https://dhanish-acxiom-demo.onrender.com/)**

The hosted demo provides an **Open demo workspace** button on the landing page. It signs into a deliberately public, fictional demo account. Manual login is also available:

- Username: `demo.admin`
- Password: `Demo@Erp2026!`

The root Dockerfile and `render.yaml` configure the deployed Render **Free** web service. No SQL Server hosting account is needed for the demo: setting `DEMO_MODE=true` uses an isolated SQLite database and seeds three fictional customers, three employees, a sample report, and settings.

Changes are shared between visitors and reset when the free host restarts or redeploys. Do not enter personal information. The demo does not process payments or send notifications.

## Run locally

Install .NET SDK 8, then from the repository root in PowerShell:

```powershell
$env:DEMO_MODE='true'
dotnet run --project AcxiomErp/AcxiomErp/AcxiomErp.csproj --no-launch-profile --urls http://127.0.0.1:18511
```

Open `http://127.0.0.1:18511`. Demo SQLite data stays in the application's working directory between local starts; the hosted container is ephemeral.

The original SQL Server path remains available when `DEMO_MODE` is absent or false. Configure `ConnectionStrings__DefaultConnection` for your own SQL Server and apply the existing EF migrations as appropriate to that database. Do not import demonstration data over a real database.

## Technology

C#, ASP.NET Core MVC / .NET 8, Entity Framework Core, SQL Server for normal configuration, SQLite for the opt-in demo, Razor, Bootstrap, HTML/CSS, and Docker.

## Demonstration scope

- Public landing page and one-click demo entry; normal username/password login also works.
- Customer and employee create, list, edit and delete operations.
- Report create, edit, delete and dashboard summaries.
- Settings records grouped by category.
- Session guard on dashboard and business-controller routes; protected POST forms use anti-forgery validation.
- Newly created passwords are hashed. Existing plaintext records can be upgraded on successful login.

Settings such as module switches are stored records; they are not a complete feature-control or two-factor authentication implementation. Sales/purchase accounting, inventory, payroll automation and GST invoices remain future work. This educational demo is not a production ERP certification.

## Verification

The .NET build passed. On 23 November 2026, all 24 focused HTTP checks passed on the actual HTTPS deployment: demo/password login, protected-page redirect, all main pages, customer/employee/report/settings create-edit-delete workflows, case-sensitive image paths and rejection of GET deletion. Each temporary test record was removed after verification. These checks are not an exhaustive production acceptance test.

The Docker context excludes the repository's old generated `bin`/`obj` files, local SQLite data and environment files. Free hosting can sleep while idle and demo changes do not persist across host restarts.
