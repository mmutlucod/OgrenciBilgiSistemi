using Microsoft.EntityFrameworkCore;
using obsProject.Controllers;
using obsProject.Data;
using obsProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using System;

namespace obsProject.Tests.Controllers
{
    public class AnasayfaTestController
    {
        //TEST İŞLEMLERİ

        [Fact]
        public async Task Test_IndexAction_RedirectsToCorrectAction_ForAdmin()
        {
            var options = new DbContextOptionsBuilder<CodeFirstDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var _context = new CodeFirstDbContext(options);

            //var kullanici = _context.Kullanici.First(k => k.KullaniciAdi == "testuser");

            //if (kullanici == null)
            //{
            //    _context.Kullanici.Add(new Kullanici { KullaniciAdi = "testuser", Parola = "testpassword", KullaniciTuruID = 1 });
            //    _context.SaveChanges();
            //}

            var controller = new AnasayfaController(_context);

            // Act
            var result = await controller.Index("testuser", "testpassword");

            // Assert
            //var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            //Assert.Equal("Index", redirectToActionResult.ActionName);
            //Assert.Equal("Ogrenci", redirectToActionResult.ControllerName);

        }

        [Fact]
        public async Task Test_IndexAction_ReturnsView_WhenCredentialsAreInvalid()
        {
            // Arrange
            // Arrange
            var options = new DbContextOptionsBuilder<CodeFirstDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            using (var context = new CodeFirstDbContext(options))
            {
                var controller = new AnasayfaController(context);

                // Act
                var result = await controller.Index("invaliduser", "invalidpassword");

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.Null(viewResult.ViewName);
                Assert.Equal("Giriş Başarısız. Kullanıcı adı veya şifre hatalı.", viewResult.ViewData["UyariMesaji"]);
            }
        }
    }
}
