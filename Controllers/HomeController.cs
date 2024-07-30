using System.Diagnostics;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TempEmbeddin2302C2.Models;

namespace TempEmbeddin2302C2.Controllers
{
    public class HomeController : Controller
    {

        private readonly _2302c2EcommerceContext db;

        public HomeController(_2302c2EcommerceContext _db)
        {
            db = _db;
        }

        //[Authorize(Roles = "User")]
        [Authorize]
        public IActionResult Index()
        {
            return View();
            //if (HttpContext.Session.GetString("role") == "user")
            //{

            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Login","Admin");
            //}
            //data can be accessed on same view
            //ViewBag.name = "Haris Naseer";
            //ViewData["email"] = "haris@gmail.com";


            ////data can be accessed on other view as well as other controllers
            //TempData["phone"] = "03241257793";

            //return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            TempData.Keep("phone");
            return View();
        }

        public IActionResult Products()
        {
            var data = db.Items.Include(item => item.Cat);
            return View(data.ToList());
        }
    }
}