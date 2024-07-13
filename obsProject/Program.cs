using obsProject.Data;
using Microsoft.EntityFrameworkCore;
//using obsProject.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.Cookies;
using PdfSharp;
using obsProject;
using PdfSharp.Fonts;
using PdfSharp.Charting;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CodeFirstDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

//app.MapGet("/", () => "Hello World!");

builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(60); // Oturum s�resi
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true; // Zorunlu olarak kabul edilen cookie
    });

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.Name = "GirisCookie";
            options.LoginPath = "/Anasayfa"; // Giri� yap�lmadan eri�ilmeye �al���lan sayfa
            options.AccessDeniedPath = "/Hata"; // Eri�im reddedildi�inde y�nlendirilecek sayfa
        });

var app = builder.Build();

app.MapControllerRoute("main", "{Controller=Anasayfa}/{Action=Index}/{id?}");

app.UseRouting();
app.UseStaticFiles();
app.UseSession();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseStatusCodePagesWithRedirects("/Hata");


app.Run();
