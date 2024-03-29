using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVC.Filter;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        [Log]
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


        public ActionResult DemoPartialViews(int? id)
        {
            return PartialView();
        }
        public ActionResult Detail(int? id)
        {
            ViewBag.Id = id;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return RedirectToAction("Index", "Home");
            //return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GrantPermission()
        {
            try
            {

                var session = HttpContext.Session["USER_ISADMIN"] ?? "";
                if (session == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                if (Convert.ToInt32(session) != 1) // Nếu không phải admin
                {
                    return RedirectToAction("Index", "Home");
                }

                // Xử lý lấy dữ liệu từ bảng UserFunction trên API

            }
            catch (Exception ex)
            {

                throw;
            }

            return View();
        }
    }
}