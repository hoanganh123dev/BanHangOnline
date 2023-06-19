using BanHangOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangOnline.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products

        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult Index(/*int? id*/)
        {
            var item = _dbContext.Products.ToList();
            //if (id != null)
            //{
            //    item = item.Where(x => x.ProductCategoryId == id).ToList();
            //}
            return View(item);
        }
        public ActionResult Detail  (string alias,int id)
        {
            var item = _dbContext.Products.Find(id);
            return View(item);
        }
        public ActionResult ProductCategory(string alias,int id)
        {
            var item = _dbContext.Products.ToList();
            if (id >0)
            {
                item = item.Where(x => x.ProductCategoryId == id).ToList();
            }
            var cate= _dbContext.ProductCategories.Find(id);
            if(cate !=null)
            {
                ViewBag.CateName = cate.Title;
            }
            ViewBag.CateId = id;
            return View(item);
        }
        public ActionResult Partial_ItemBycateId()
        {
            var item = _dbContext.Products.Where(x => x.IsHome && x.IsActive).Take(14).ToList();
            return PartialView( item);
        }
        public ActionResult Partial_ProductSales()
        {
            var item = _dbContext.Products.Where(x => x.IsSale && x.IsActive).Take(14).ToList();
            return PartialView(item);
        }
    }
}