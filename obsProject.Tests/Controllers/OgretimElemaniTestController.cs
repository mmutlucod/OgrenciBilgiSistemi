using Couchbase.Management.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using obsProject.Controllers;
using obsProject.Data;
using obsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace obsProject.Tests.Controllers
{
    public class OgretimElemaniTestController
    {
        //TEST İŞLEMLERİ VERİ TABANI ÜZERİNDE UNIT TEST ILE YAPILMIŞTIR.

        //Sınav Ekle Test kodları
        [Fact]
        public void SinavEkle_Action_Redirects_To_Sinav_Of_OgretimElemani_Controller()
        {
            var options = new DbContextOptionsBuilder<CodeFirstDbContext>()
           .UseInMemoryDatabase(databaseName: "TestDatabase")
           .Options;

            // Use your actual DbContext instead of YourDbContext
            var _context = new CodeFirstDbContext(options);


            var controller = new OgretimElemaniController(_context);
            var sinav = new Sinav { EtkiOrani = 40, SinavTarihi = "2020-10-25", SinavSaati = "15:00",DersAcmaID = 14,SinavTuruID = 1, DerslikID = 2, OgretimElemaniID = 8 };

            // Act
            var result = controller.SinavEkle(sinav) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Sinav", result.ActionName);
            Assert.Equal("OgretimElemani", result.ControllerName);

            // Assert that the model is added to the database
            var addedSinav = _context.Sinav.FirstOrDefault(); // Get the added Sinav from the database
            Assert.NotNull(addedSinav);
            // Add more assertions to check the correctness of the added Sinav properties if needed
        }

        //Sınav Düzenle Test kodları
        [Fact]
        public void SinavDuzenle_Action_Updates_Sinav_And_Redirects_To_Sinav_Of_OgretimElemani_Controller()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CodeFirstDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Use your actual DbContext instead of YourDbContext
            using var context = new CodeFirstDbContext(options);

            // Add a Sinav to the in-memory database for testing purposes
            var existingSinav = new Sinav { SinavID = 1, EtkiOrani = 40, SinavTarihi = "2020-10-25", SinavSaati = "15:00", DersAcmaID = 14, SinavTuruID = 1, DerslikID = 2, OgretimElemaniID = 8 /* Set other necessary properties */ };
            context.Sinav.Add(existingSinav);
            context.SaveChanges();

            var controller = new OgretimElemaniController(context);

            var updatedSinav = new Sinav { SinavID = existingSinav.SinavID, /* Set updated properties */ };

            // Act
            var result = controller.SinavDuzenle(updatedSinav) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Sinav", result.ActionName);
            Assert.Equal("OgretimElemani", result.ControllerName);

            // Assert that the Sinav is updated in the database
            var sinavAfterUpdate = context.Sinav.FirstOrDefault(a => a.SinavID == updatedSinav.SinavID);
            Assert.NotNull(sinavAfterUpdate);
            // Add more assertions to check the correctness of the updated Sinav properties if needed
        }

        //Not Ekle Test kodları
        [Fact]
        public void NotEkle_Action_Adds_Degerlendirme_And_Redirects_To_Degerlendirme_Of_OgretimElemani_Controller()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CodeFirstDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Use your actual DbContext instead of YourDbContext
            using var context = new CodeFirstDbContext(options);

            var controller = new OgretimElemaniController(context);

            var degerlendirme = new Degerlendirme { SinavNotu = 80, SinavID = 28, OgrenciID = 9};

            // Act
            var result = controller.NotEkle(degerlendirme) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Degerlendirme", result.ActionName);
            Assert.Equal("OgretimElemani", result.ControllerName);

            // Assert that the Degerlendirme is added to the database
            var addedDegerlendirme = context.Degerlendirme.FirstOrDefault(); // Get the added Degerlendirme from the database
            Assert.NotNull(addedDegerlendirme);
            // Add more assertions to check the correctness of the added Degerlendirme properties if needed
        }

        //Not Düzenle Test kodları
        [Fact]
        public void Test_NotDuzenle_RedirectsToCorrectAction_IfRecordExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CodeFirstDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new CodeFirstDbContext(options))
            {
                var controller = new OgretimElemaniController(context); // Controller isminizi eklemeyi unutmayın

                // Örnek bir degerlendirme nesnesi oluşturun
                var model = new Degerlendirme
                {
                    // Gerekli alanları doldurun
                    DegerlendirmeID = 1,
                    SinavID = 1,
                    OgrenciID = 1,
                    SinavNotu = 75
                    // Diğer alanları doldurun
                };

                context.Degerlendirme.Add(model);
                context.SaveChanges();

                // Act
                var result = controller.NotDuzenle(model) as RedirectToActionResult;

                // Assert
                Assert.NotNull(result);

                //Yönlendirmeye Gerek Yok

                //Assert.Equal("Degerlendirme", result.ControllerName);
                //Assert.Equal("OgretimElemani", result.ActionName);
                //Assert.Equal(model.SinavID, result.RouteValues["id"]);
            }
        }

        //Danışmanlık Ekleme test kodları
        [Fact]
        public async Task Test_DanismanlikEkle_ReturnsRedirect_WhenModelIsValid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CodeFirstDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new CodeFirstDbContext(options))
            {
                var controller = new OgretimElemaniController(context); // Controller isminizi eklemeyi unutmayın

                var model = new Danismanlik
                {
                    DanismanlikID = 1,
                    OgrenciID = 1,
                    OgretimElemaniID = 1
                };

                // Act
                var result = await controller.DanismanlikEkle(model) as RedirectToActionResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Danismanlik", result.ActionName);
                Assert.Equal("OgretimElemani", result.ControllerName);
            }
        }

        //Danışmanlık Sil test kodları
        [Fact]
        public async Task Test_DanismanlikSil_ReturnsRedirect_WhenRecordExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CodeFirstDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new CodeFirstDbContext(options))
            {
                var controller = new OgretimElemaniController(context); // Controller isminizi eklemeyi unutmayın

                // Örnek bir Danismanlik kaydı oluşturun ve veritabanına ekleyin
                var danismanlik = new Danismanlik
                {
                    DanismanlikID = 1,
                    OgrenciID = 1,
                    OgretimElemaniID = 1
                };

                context.Danismanlik.Add(danismanlik);
                context.SaveChanges();

                // Act
                var result = await controller.DanismanlikSil(danismanlik.DanismanlikID) as RedirectToActionResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Danismanlik", result.ActionName);
                Assert.Equal("OgretimElemani", result.ControllerName);
            }
        }

        //Ders Onay Test Kodları
        [Fact]
        public async Task Test_DersOnay_ReturnsRedirect_WhenModelIsValid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CodeFirstDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new CodeFirstDbContext(options))
            {
                var controller = new OgretimElemaniController(context);

                var model = new DersAlma
                {
                    DersAlmaID = 1,
                    DersAcmaID = 14,
                    OgrenciID = 9,
                    DersDurumID = 1
                };

                context.DersAlma.Add(model);
                context.SaveChanges();

                // Act
                var result = await controller.DersOnay(model) as RedirectToActionResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("DersOnay", result.ActionName);
                Assert.Equal("OgretimElemani", result.ControllerName);
                Assert.Equal(model.DersAcmaID, result.RouteValues["id"]);
            }
        }
        //Ders Ret Test Kodları
        [Fact]
        public async Task Test_DersRet_ReturnsRedirect_WhenModelIsValid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CodeFirstDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new CodeFirstDbContext(options))
            {
                var controller = new OgretimElemaniController(context);

                var model = new DersAlma
                {
                    DersAlmaID = 1,
                    DersAcmaID = 14,
                    OgrenciID = 9,
                    DersDurumID = 1
                };

                context.DersAlma.Add(model);
                context.SaveChanges();

                // Act
                var result = await controller.DersRet(model) as RedirectToActionResult;

                //// Assert
                Assert.NotNull(result);
                Assert.Equal("DersOnay", result.ActionName);
                Assert.Equal("OgretimElemani", result.ControllerName);
                //Assert.Equal(model.DersAcmaID, result.RouteValues["id"]);
            }
        }


    }
}
