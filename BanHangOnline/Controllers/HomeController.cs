using BanHangOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanHangOnline.Models;

namespace BanHangOnline.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult Partial_Subscribe()
        {
            return PartialView();  
        }
        [HttpPost]
        public ActionResult Subcribe(Subscribe rq)
        {
            if(ModelState.IsValid)
            {
                _dbContext.Subscribes.Add(new Subscribe { Email= rq.Email,CreatedDate = DateTime.Now});
                _dbContext.SaveChanges();
                return Json(new {Success=true }); 
            }
            return View("Partial_Subscribe",rq);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}