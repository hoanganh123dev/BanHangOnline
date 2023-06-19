using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using BanHangOnline.Models;
using BanHangOnline.Models.EF;
using PagedList;

namespace BanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class NewsController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/News
        public ActionResult Index(string SearchText, int? page)
        {
           
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<News> items = _dbContext.News.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(SearchText))
            {
               items= items.Where(x=>x.Alias.Contains(SearchText) || x.Title.Contains(SearchText));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page):1;
            items= items.ToPagedList(pageIndex,pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(News model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.CategoryId = 4;
                model.ModifiedDate = DateTime.Now;              
                model.Alias = BanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                _dbContext.News.Add(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int Id)
        {
            var item = _dbContext.News.Find(Id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News model)
        {
            if (ModelState.IsValid)
            {
                _dbContext.News.Attach(model);
                model.ModifiedDate = DateTime.Now;
                model.Alias = BanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                _dbContext.Entry(model).State = System.Data.Entity.EntityState.Modified;           
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbContext.News.Find(id);
            if (item != null)
            {
                //var DeleteItem = _dbConnect.Categories.Attach(item);
                _dbContext.News.Remove(item);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = _dbContext.News.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }

            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if(!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if(items !=null && items.Any())
                {
                    foreach(var item in items)
                    {
                        var obj = _dbContext.News.Find(Convert.ToInt32(item));
                        _dbContext.News.Remove(obj);
                        _dbContext.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });

        }
    }
}