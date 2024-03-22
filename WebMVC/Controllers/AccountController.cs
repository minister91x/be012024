using MyShop.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class AccountController : Controller
    {
        // GET: Accountontroller
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public JsonResult Account_Login(AccountLoginRequestData requestData)
        {
            var returnData = new LoginResponseData();
            try
            {
                var url = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "";
                var baseUrl = "Account/Account_Login";
                // chuyển dổi data từ object sang json 

                var bodyJson = JsonConvert.SerializeObject(requestData);

                var result = HttpRequestHelper.SendPost(url, baseUrl, bodyJson);

                // convert ngược lại từ string dang json sang Object LoginResponseData

                returnData = JsonConvert.DeserializeObject<LoginResponseData>(result);

                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}