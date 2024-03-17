using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;
using System.Text;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebApp.Data;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AccountContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("MarketManagement"));
});

builder.Services.AddDbContext<MarketContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("MarketManagement"));
});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<AccountContext>()
        .AddDefaultTokenProviders();


builder.Services.AddRazorPages();

builder.Services.AddScoped<CategorySQLRepository>();
builder.Services.AddScoped<ProductSQLRepository>();
builder.Services.AddScoped<TransactionSQLRepository>();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Inventory", p => p.RequireClaim("Position", "Inventory"));
    options.AddPolicy("Cashier", p => p.RequireClaim("Position", "Cashier"));
    options.AddPolicy("Admin", p => p.RequireClaim("Position", "Admin"));
});


var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//         name of the controller/ name of the action method /optional

app.MapControllerRoute(
    name:"areas",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

app.Run();