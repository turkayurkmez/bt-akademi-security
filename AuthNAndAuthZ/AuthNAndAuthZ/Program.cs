using AuthNAndAuthZ.DataContext;
using AuthNAndAuthZ.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.LoginPath = "/Kullanici/Giris";
                    option.ReturnUrlParameter = "gidilecekAdres";
                    option.AccessDeniedPath = "/Kullanici/ErisimEngellendi";
                });

var connectionString = builder.Configuration.GetConnectionString("db");
builder.Services.AddDbContext<SecureDbContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddScoped<IUserService, RealUserService>();
                
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
