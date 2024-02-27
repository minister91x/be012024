using DataAccess.ProductNetFrameWork.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppintCart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListShoppingCartPartial()
        {
            
            var list = new List<ShoppingCart>();
            try
            {
                var cookie = Request.Cookies["MyShoppingCart"] != null ? Request.Cookies["MyShoppingCart"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ShoppingCart>>(HttpUtility.UrlDecode(cookie));
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return PartialView(list);
        }

        
        public JsonResult CreateOrder(OrderInsertRequestData requestData)
        {
            var list = new List<ShoppingCart>();
            try
            {
                var cookie = Request.Cookies["MyShoppingCart"] != null ? Request.Cookies["MyShoppingCart"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ShoppingCart>>(HttpUtility.UrlDecode(cookie));
                }
                // kiểm tra xem số lượng của từng mặt hàng còn không 

                // Tạo đơn hàng ( giỏ hàng + dữ liệu của khách hàng)
                var ma_dh = CreateOrder(requestData, list);



                // Tạo chi tiết đơn hàng 
                var list_orderDetail = new List<OrderDetail>();
                foreach (var item in list)
                {
                    list_orderDetail.Add(new OrderDetail
                    {
                        Ma_DonHang = ma_dh,
                        ProductId = item.ProductID,
                        ProductName = item.ProductName,
                        Quantity = item.Quality
                    }) ;
                }

                return Json(list_orderDetail, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exx)
            {

                throw;
            }
        }

        private string CreateOrder(OrderInsertRequestData requestData, List<ShoppingCart> carts)
        {
            try
            {
                var totalAmount = 0;
                foreach (var item in carts)
                {
                    totalAmount += item.Price;
                }

                var cart = new Order();
                cart.MaDonHang = "DH_" + Guid.NewGuid().ToString();
                cart.TotalAmount = totalAmount;
                //cart.TenKhachHang = requestData.CustomerName;

                return cart.MaDonHang;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}