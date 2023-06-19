using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanHangOnline.Models;

namespace BanHangOnline.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuTop()
        {
            var item = _dbContext.Categories.OrderBy(x => x.Position).ToList();
            return PartialView("_MenuTop",item);
        }
        public ActionResult MenuProductCategory()
        {
            var item = _dbContext.ProductCategories.ToList();
            return PartialView("_MenuProductCategory", item);
        }
        public ActionResult MenuLeft(int? id)
        {
            if(id !=null)
            {
                ViewBag.CateId = id;
            }
            var item = _dbContext.ProductCategories.ToList(); 
            return PartialView("_MenuLeft", item);
        }
        public ActionResult MenuArrivals()
        {
            var item = _dbContext.ProductCategories.ToList();
            return PartialView("_MenuArrivals", item);
        }
    }
}