using BanHangOnline.Models;
using BanHangOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class ProductImageController : Controller
    {
        // GET: Admin/ProductImage
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public ActionResult Index(int id)
        {
            ViewBag.ProductId = id;
            var items= _dbContext.ProductImages.Where(x=> x.ProductId == id).ToList();   
            return View(items);
        }
        [HttpPost]
        public ActionResult CreateImage(int productId, string url)
        {
            _dbContext.ProductImages.Add(new ProductImage
            {
                ProductId = productId,
                Image = url,
                IsDefault = false
            });
            _dbContext.SaveChanges();
            return Json(new { success = true });
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbContext.ProductImages.Find(id);     
                _dbContext.ProductImages.Remove(item);
                _dbContext.SaveChanges();
            return Json(new {success = true });
        }
    }
}