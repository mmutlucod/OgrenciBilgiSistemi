using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using obsProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using obsProject.Controllers;
using Microsoft.EntityFrameworkCore;
using obsProject.Data;
using Microsoft.AspNetCore.Hosting;
using obsProject.Models;
using Moq;

namespace obsProject.Tests.Controllers
{
    public class OgrenciTestController
    {

        //[Fact]
        //public void Test_DersEkle_AddsCourse_WhenNotAlreadySelected()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<CodeFirstDbContext>()
        //        .UseInMemoryDatabase(databaseName: "TestDatabase")
        //        .Options;

        //    using (var context = new CodeFirstDbContext(options))
        //    {
        //        var controller = new OgrenciController(context, Mock.Of<IWebHostEnvironment>());

        //        var dersAlma = new DersAlma
        //        {
        //            DersAlmaID = 1,
        //            DersAcmaID = 123, // Assuming specific IDs for the test
        //            OgrenciID = 456,
        //            // ... other necessary properties
        //        };

        //        // Act
        //        var result = controller.DersEkle(dersAlma) as RedirectToActionResult;

        //        // Assert
        //        Assert.NotNull(result);
        //        Assert.Equal("DersKayit", result.ActionName);
        //        Assert.Equal("Ogrenci", result.ControllerName);

        //        // Check if the course is added correctly by inspecting the database context
        //        var addedCourse = context.DersAlma.FirstOrDefault(x => x.DersAcmaID == 123 && x.OgrenciID == 456);
        //        Assert.NotNull(addedCourse); // Verify that the course is added to the database
        //    }
        //}




        //Öğrenci Ders Çıkar Test Kodu
        [Fact]
        public void Test_DersSil_RemovesCourse_IfExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CodeFirstDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new CodeFirstDbContext(options))
            {
                // Adding a dummy course for deletion testing
                context.DersAlma.Add(new DersAlma
                {
                    DersAlmaID = 1,
                    DersAcmaID = 123, // Assuming specific IDs for the test
                    OgrenciID = 456,
                    DersDurumID = 3
                    // ... other necessary properties
                });
                context.SaveChanges();

                var hostingEnvironmentMock = new Mock<IWebHostEnvironment>();

                var controller = new OgrenciController(context, hostingEnvironmentMock.Object);

                var dersAlma = new DersAlma
                {
                    DersAlmaID = 1,
                    DersAcmaID = 123, // Assuming specific IDs for the test
                    OgrenciID = 456,
                    DersDurumID = 3
                };

                // Act
                var result = controller.DersSil(dersAlma) as RedirectToActionResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("DersKayit", result.ActionName);
                Assert.Equal("Ogrenci", result.ControllerName);
                // Check if the course is removed correctly by inspecting the database context
                var removedCourse = context.DersAlma.FirstOrDefault(x => x.DersAlmaID == 1);
                Assert.Null(removedCourse);
            }
        }


    }
}
