using BanHangOnline.Models;
using BanHangOnline.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class ProductController : Controller
    {
        // GET: Admin/Product
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult Index(string SearchText, int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Product> items = _dbContext.Products.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(SearchText))
            {
                items = items.Where(x => x.Alias.Contains(SearchText) || x.Title.Contains(SearchText));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }
        public ActionResult Create()
        {
            ViewBag.ProductCategory = new SelectList(_dbContext.ProductCategories.ToList(),"Id","Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product model, List<string> Images, List<int> rDefaut)
        {
            if (ModelState.IsValid)
            {
                if(Images !=null && Images.Count >0)
                {
                    for(int i=0; i < Images.Count; i++)
                    {
                        if(i + 1 == rDefaut[0])
                        {
                            model.Image = Images[i];
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId =model.Id,
                                Image= Images[i],
                                IsDefault =true
                                
                            });
                        }else
                        {
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = false

                            });
                        }
                    }
                }
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                if(string.IsNullOrEmpty(model.SeoTitle))
                {
                    model.SeoTitle = model.Title;
                }
                    if (string.IsNullOrEmpty(model.Alias))
                        model.Alias = BanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                _dbContext.Products.Add(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = new SelectList(_dbContext.ProductCategories.ToList(), "Id", "Title");
            return View(model);
        }
        public ActionResult Edit(int Id)
        {
            ViewBag.ProductCategory = new SelectList(_dbContext.ProductCategories.ToList(), "Id", "Title");
            var item = _dbContext.Products.Find(Id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Products.Attach(model);
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
            var item = _dbContext.Products.Find(id);
            if (item != null)
            {
                //var DeleteItem = _dbConnect.Categories.Attach(item);
                _dbContext.Products.Remove(item);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = _dbContext.Products.Find(id);
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
        public ActionResult IsHome(int id)
        {
            var item = _dbContext.Products.Find(id);
            if (item != null)
            {
                item.IsHome = !item.IsHome;
                _dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return Json(new { success = true, IsHome = item.IsHome });
            }

            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsSale(int id)
        {
            var item = _dbContext.Products.Find(id);
            if (item != null)
            {
                item.IsSale = !item.IsSale;
                _dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return Json(new { success = true, IsSale = item.IsSale });
            }

            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
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