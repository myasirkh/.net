﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using TempEmbeddin2302C2.Models;

namespace TempEmbeddin2302C2.Controllers
{
	public class ItemController : Controller
	{
		//_2302c2ecommerceContext db = new _2302c2ecommerceContext();

		private readonly _2302c2EcommerceContext db;

		public ItemController(_2302c2EcommerceContext _db)
		{
			db = _db;
		}

		public IActionResult Index()
		{
			var data= db.Items.Include(item => item.Cat);
			return View(data.ToList());
		}

        public IActionResult Create()
        {
            ViewBag.CatId = new SelectList(db.Categories, "CatId", "CatName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Item item, IFormFile file)
        {
            string imagename = DateTime.Now.ToString("yymmddhhmmss");
            imagename += "-" + Path.GetFileName(file.FileName);

            var imagepath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Uploads");
            var imageValue = Path.Combine(imagepath, imagename);

            using (var stream = new FileStream(imageValue, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var dbimage = Path.Combine("/Uploads", imagename);

            item.Image = dbimage;

            db.Items.Add(item);
            db.SaveChanges();

            ViewBag.CatId = new SelectList(db.Categories, "CatId", "CatName");
            return RedirectToAction("Index");

        }
        public IActionResult Edit(int id)
        {
            var item = db.Items.Find(id);
            if(item != null)
            {
            ViewBag.CatId = new SelectList(db.Categories, "CatId", "CatName");
            return View(item);

            }
            else
            {
                ViewBag.errorMsg = "Product not available.";
                return RedirectToAction();
            }
        }
        [HttpPost]
        public IActionResult Edit(Item item, IFormFile file, string oldImage)
        {if (file != null && file.Length > 0)
            {
                string imagename = DateTime.Now.ToString("yymmddhhmmss");
                imagename += "-" + Path.GetFileName(file.FileName);

                var imagepath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Uploads");
                var imageValue = Path.Combine(imagepath, imagename);

                using (var stream = new FileStream(imageValue, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                var dbimage = Path.Combine("/Uploads", imagename);

                item.Image = dbimage;

                db.Items.Update(item);
                db.SaveChanges();
            }
            else
            {
                item.Image = oldImage;

                db.Items.Update(item);
                db.SaveChanges();
            }
                ViewBag.CatId = new SelectList(db.Categories, "CatId", "CatName");
                return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var item = db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
