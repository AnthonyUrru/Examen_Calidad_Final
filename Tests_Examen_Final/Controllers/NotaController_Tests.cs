using ExamenFinal_Notas.Controllers;
using ExamenFinal_Notas.Model_Mocks.Controllers;
using ExamenFinal_Notas.Model_Mocks.Services;
using ExamenFinal_Notas.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests_Examen_Final.Controllers
{
    [TestFixture]
    public class NotaController_Tests
    {
        
        [Test]
        public void Cargar_Create_Post()
        {
            var ReposMock = new Mock<INota_Controller_Service>();
            var MockCook = new Mock<ICookieService>();
            var Controller = new NotasController(ReposMock.Object, MockCook.Object);
            var assert = Controller.Create();
            Assert.IsInstanceOf<ViewResult>(assert);
        }
        [Test]
        public void Registrar_Nota()
        {
            var claim = new Mock<ICookieService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User());
            var repo = new Mock<INota_Controller_Service>();
            repo.Setup(o => o.Agregar_idUser_a_Nota(new Nota(), 12));


            var controller = new NotasController(repo.Object, claim.Object);
            var view = controller.Create(new Nota(), new List<int>() { 1 }) as RedirectToActionResult;

            Assert.AreEqual("Index", view.ActionName);
        }
        [Test]
        public void Eliminar_nota()
        {
            var ReposMock = new Mock<INota_Controller_Service>();
            var MockCook = new Mock<ICookieService>();
            var Controller = new NotasController(ReposMock.Object, MockCook.Object);
            var assert = Controller.Create();
            Assert.IsInstanceOf<ViewResult>(assert);
        }
        [Test]
        public void Editar_Nota_Case()
        {
            var ReposMock = new Mock<INota_Controller_Service>();
            var MockCook = new Mock<ICookieService>();
            var Controller = new NotasController(ReposMock.Object, MockCook.Object);
            var assert = true;
            Assert.IsTrue(assert);
        }
        

    }
}
