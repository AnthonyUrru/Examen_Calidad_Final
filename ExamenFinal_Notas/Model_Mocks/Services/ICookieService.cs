using ExamenFinal_Notas.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExamenFinal_Notas.Model_Mocks.Services
{
    public interface ICookieService
    {
        public User GetLoggedUser();
        void SetHttpContext(HttpContext httpContext);
    public void Login_User(ClaimsPrincipal claim);
    public Claim Obtener_ClaimPrincipal();
}
public class Cookie_Service : ICookieService
{
        private readonly NotasContext _context;
        private HttpContext httpContext;
        public Cookie_Service(NotasContext _context)
        {
        this._context = _context;
        }
    public Claim Obtener_ClaimPrincipal()
    {
        var claim = httpContext.User.Claims.FirstOrDefault();
        return claim;
    }
    public void SetHttpContext(HttpContext httpContext)
    {
        this.httpContext = httpContext;
        
    }

    public void Login_User(ClaimsPrincipal claim)
    {
        httpContext.SignInAsync(claim);

    }
        public User GetLoggedUser()
        {
            var claim = httpContext.User.Claims.FirstOrDefault();
            var user = _context.Users.Where(o => o.Username == claim.Value).FirstOrDefault();
            return user;

        }


    }
}
