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
    
        public class UserData
        {
            public User userData1()
            {
                var user = new User();
                user.Username = "Anthony1";
                user.Password = "12345";
                return user;
            }
            public User userData2()
            {
                var user = new User();
                user.Username = "raul";
                user.Password = "raulF";
                return user;
            }

        }
        [TestFixture]
        public class AuthController_Tests
        {
            [Test]
            public void Iniciar_SesionCOrrecta()
            {

                var RepoMock = new Mock<INota_Controller_Service>();
                var n = new UserData();
                RepoMock.Setup(o => o.Usser_lookfor(n.userData1().Username, n.userData1().Password)).Returns(n.userData1());
                var CookieMock = new Mock<ICookieService>();
                var authCont = new AuthController(RepoMock.Object, CookieMock.Object);
                var assert = authCont.Login("Anthony1", "12345");
                Assert.IsInstanceOf<RedirectToActionResult>(assert);

            }
            [Test]
            public void Iniciar_SessionIncorrecta()
            {

                var n = new UserData();
                var userMock = new Mock<INota_Controller_Service>();
                userMock.Setup(o => o.Usser_lookfor(n.userData2().Username, n.userData2().Password)).Returns(n.userData2());

                var cookMock = new Mock<ICookieService>();
                var authCont = new AuthController(userMock.Object, cookMock.Object);
                var log = authCont.Login("raul", "raulito");

                Assert.IsInstanceOf<ViewResult>(log);
            }

        [Test]
        public void Cargar_Register_Correcto()
        {
            var ReposMock = new Mock<INota_Controller_Service>();
            var MockCook = new Mock<ICookieService>();
            var Controller = new AuthController(ReposMock.Object, MockCook.Object);
            var assert = Controller.Register();
            Assert.IsInstanceOf<ViewResult>(assert);
        }

        [Test]
        public void RegisterPostGood()
        {
            var repo = new Mock<INota_Controller_Service>();
            var cookMock = new Mock<ICookieService>();
            repo.Setup(o => o.Usser_lookfor("Anthony1","12345")).Returns(new User());
            repo.Setup(o => o.Register_User("Anthony1","12345"));
            var controller = new AuthController(repo.Object,cookMock.Object);
            var a = new User();
            var view = controller.Register(a,"Anthonyy","123456789") as RedirectToActionResult;
            Assert.AreEqual("Login", view.ActionName);

        }



    }

}
