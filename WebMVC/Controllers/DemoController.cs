using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class DemoController : Controller
    {
        // GET: Demo
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult JsonData(DataModel data)
        {
            var list = new List<DataModel>();
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    list.Add(new DataModel { Id = i, Name = "ASPNET " + i });
                }

                var ifExist = list.Where(s=>s.Id== data.Id).ToList();

                if (ifExist.Count > 0)
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }

                else
                {

                    return Json(new List<DataModel>(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}