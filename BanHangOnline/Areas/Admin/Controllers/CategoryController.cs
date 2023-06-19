using BanHangOnline.Models;
using BanHangOnline.Models.EF;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        private ApplicationDbContext _dbConnect = new ApplicationDbContext();
        public ActionResult Index()
        {
            var items = _dbConnect.Categories;
            return View(items);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category model)
        {
            if(ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = BanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                _dbConnect.Categories.Add(model);
                _dbConnect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int Id)
        {
            var item = _dbConnect.Categories.Find(Id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                _dbConnect.Categories.Attach(model);
                model.ModifiedDate = DateTime.Now;
                model.Alias = BanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                _dbConnect.Entry(model).Property(x => x.Title).IsModified = true;
                _dbConnect.Entry(model).Property(x => x.Description).IsModified = true;
                _dbConnect.Entry(model).Property(x => x.Alias).IsModified = true;
                _dbConnect.Entry(model).Property(x => x.SeoTitle).IsModified = true;
                _dbConnect.Entry(model).Property(x => x.SeoDescription).IsModified = true;
                _dbConnect.Entry(model).Property(x => x.SeoKeyWords).IsModified = true;
                _dbConnect.Entry(model).Property(x => x.Position).IsModified = true;
                _dbConnect.Entry(model).Property(x => x.ModifiedDate).IsModified = true;
                _dbConnect.Entry(model).Property(x => x.Modifiedby).IsModified = true;
                _dbConnect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbConnect.Categories.Find(id);
            if(item !=null)
            {
                //var DeleteItem = _dbConnect.Categories.Attach(item);
                _dbConnect.Categories.Remove(item);
                _dbConnect.SaveChanges();
                return Json(new { success=true});
            }
            return Json(new { success = false });
        }
    }
}