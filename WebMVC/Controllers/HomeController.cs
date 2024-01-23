using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            // try + tab 2 lần
            var list = new List<DataModel>();
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    list.Add(new DataModel { Id = i, Name = "ASPNET " + i });
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return View(list);
        }

        public ActionResult Detail(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            //return RedirectToAction("Index", "Home");
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}