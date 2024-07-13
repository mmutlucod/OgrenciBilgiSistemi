using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using obsProject.Data;

namespace obsProject.Controllers
{
    public class AnasayfaController : Controller
    {
        private CodeFirstDbContext _context;

        public AnasayfaController(CodeFirstDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("User"))
            {
                return RedirectToAction("Index", "Ogrenci");
            }
            else if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "OgretimElemani");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(string adi, string parola)
        {
            var kullanici = _context.Kullanici.FirstOrDefault(k => k.KullaniciAdi == adi && k.Parola == parola);
            if (kullanici != null && kullanici.KullaniciTuruID == 1)
            {
                var kullaniciTuru = _context.KullaniciTuru.FirstOrDefault(k => k.KullaniciTuruID == kullanici.KullaniciTuruID);

                var ogrenci = _context.Ogrenci.FirstOrDefault(x => x.KullaniciID == kullanici.KullaniciID);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, (ogrenci.Adi + " " + ogrenci.Soyadi).ToString()),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim("KullaniciTuru", kullaniciTuru.KullaniciTuruAdi.ToString()),
                    new Claim("KullaniciID", kullanici.KullaniciID.ToString())
                };


                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent =  true // Opsiyonel: Kalıcı bir cookie oluşturmak için
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );

                return RedirectToAction("Index", "Ogrenci");
            }
            else if (kullanici != null && kullanici.KullaniciTuruID == 2)
            {
                var kullaniciTuru = _context.KullaniciTuru.FirstOrDefault(k => k.KullaniciTuruID == kullanici.KullaniciTuruID);

                var ogretimElemani = _context.OgretimElemani.FirstOrDefault(oe => oe.KullaniciID == kullanici.KullaniciID);

                var unvan = _context.Unvan.FirstOrDefault(x => x.UnvanID == ogretimElemani.UnvanID);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, (" " + ogretimElemani.Adi + " " + ogretimElemani.Soyadi).ToString()),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("KullaniciTuru", kullaniciTuru.KullaniciTuruAdi.ToString()),
                    new Claim("KullaniciID", kullanici.KullaniciID.ToString())
                };


                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true // Opsiyonel: Kalıcı bir cookie oluşturmak için
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );

                return RedirectToAction("Index", "OgretimElemani");
            }
            else
            {
                ViewBag.UyariMesaji = "Giriş Başarısız. Kullanıcı adı veya şifre hatalı.";
                return View();
            }
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
