using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace TempEmbeddin2302C2.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string pass)
        {
            bool isAuthenticated = false;
            bool isAdmin = false;
            ClaimsIdentity identity = null;

            if (email == "admin@gmail.com" && pass == "123")
            {
                identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name,"Haris" ),
                    new Claim(ClaimTypes.Role, "Admin")
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                isAdmin = true;
                isAuthenticated = true;


            }
            else if (email == "user@gmail.com" && pass == "123")
            {
                identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name,"user1" ),
                    new Claim(ClaimTypes.Role, "User")
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                isAdmin = false;
                isAuthenticated = true;

                //return RedirectToAction("Index", "Home");
            }
            if(isAuthenticated && isAdmin)
            {
                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Admin");

            }
            else if (isAuthenticated)
            {
                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
            
        }

        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Auth");
        }
    }
}
