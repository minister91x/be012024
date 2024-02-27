using DataAccess.ProductNetFrameWork.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListOrderPartial(ListCartRequestData requestData)
        {
            var list = new List<Order>();
            try
           {
                requestData.FromDate = new DateTime(2020, 1, 1);
                requestData.ToDate = new DateTime(2025, 1, 1);

                list = new DataAccess.ProductNetFrameWork.DAOImpl.OrderDAOImpl().GetListOrder(requestData.MaDH, requestData.FromDate, requestData.ToDate);
            }
            catch (Exception ex)
            {

                throw;
            }

            return PartialView(list);
        }

        public ActionResult OrderDetail()
        {
            return View();
        }
    }
}