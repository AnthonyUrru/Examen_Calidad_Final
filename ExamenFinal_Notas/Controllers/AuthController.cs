using ExamenFinal_Notas.Model_Mocks.Controllers;
using ExamenFinal_Notas.Model_Mocks.Services;
using ExamenFinal_Notas.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ExamenFinal_Notas.Controllers
{
    public class AuthController : Controller
    {

      

        private readonly IConfiguration configuration;
        private readonly ICookieService _cook;
        private readonly INota_Controller_Service _repos;
        
        //private readonly NotasContext context;

        public AuthController(INota_Controller_Service _repos, ICookieService _cook)
        {
            //this.configuration = configuration;
            //this.context = context;
            this._repos = _repos;
            this._cook = _cook;
        }


        [HttpGet]
        public string Index(string input)
        {
            return CreateHash(input);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register() // GET
        {
            return View("Register", new User());

        }

        [HttpPost]
        public ActionResult Register(User usuario, string username, string contraseña) // POST
        {


            if (ModelState.IsValid)
            {
                _repos.Register_User(username,contraseña);
                return RedirectToAction("Index", "Home");
            }
            return View("Register", usuario);
        }

        [HttpPost]

        public IActionResult Login(string username, string contraseña)
        {
            //var user = context.Users.Where(o => o.Username == username && o.Password == contraseña).FirstOrDefault();
            var user = _repos.Usser_lookfor(username,contraseña);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                _cook.SetHttpContext(HttpContext);
                _cook.Login_User(claimsPrincipal);
               // HttpContext.SignInAsync(claimsPrincipal);
                
                return RedirectToAction("Index","Home");
                
            }
            // ModelState.AddModelError("Login", "Usuario o contraseña incorrectos");

            return View();
        }
        /*{
        public IActionResult Login(string username, string password)
        /*
            /* valid5ar si el usuario existe en la base de datos y 
             * verificar que el password sea correcto*/

                //var user = repository.FindUser(username, CreateHash(password));

                // if (user != null)
                // {
                // Autenticaremos
                //    var claims = new List<Claim> {
                //       new Claim(ClaimTypes.Name, username)
                //  };

                //** var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // cree la cookie y permita la autenticación
                // cookieAuthService.SetHttpContext(HttpContext);
                //cookieAuthService.Login(claimsPrincipal);

                //    return RedirectToAction("Index", "Home");
                //  }

                // ModelState.AddModelError("Login", "Usuario o contraseña incorrectos.");
                // return View();
                // }

                // [HttpGet]
                //public IActionResult Logout()
                //{
                // HttpContext.SignOutAsync();
                //   return RedirectToAction("Login");
                // }
                public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private string CreateHash(string input)
        {
            var sha = SHA256.Create();
            input += configuration.GetValue<string>("Token");
            var hash = sha.ComputeHash(Encoding.Default.GetBytes(input));

            return Convert.ToBase64String(hash);
        }
    }
}
