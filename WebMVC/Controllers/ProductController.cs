using DataAccess.ProductNetFrameWork.DTO;
using MyShop.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMVC.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListProductPartial(ProductGetListRequestData requestData)
        {
            var list = new List<ProductFromAPI>();
            try
            {
                var url = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "";
                var baseUrl = "Product/Product_Getlist";

                var bodyJson = JsonConvert.SerializeObject(requestData);

                var result = HttpRequestHelper.SendPost(url, baseUrl, bodyJson);
                list = JsonConvert.DeserializeObject<List<ProductFromAPI>>(result);

                // list = new DataAccess.ProductNetFrameWork.DAOImpl.ProductDAOImpl().GetProducts(requestData);
            }
            catch (Exception ex)
            {

                throw;
            }

            return PartialView(list);


        }

        public ActionResult ProductInsertUpdate(int? id)
        {
            var model = new Product();
            try
            {
                if (id != null)
                {
                    model = new DataAccess.ProductNetFrameWork.DAOImpl.ProductDAOImpl().GetProductById(Convert.ToInt32(id));
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return View(model);
        }

        [ValidateAntiForgeryToken]
        public JsonResult Product_InsertUpdate(Product product)
        {
            var returnData = new ReturnData();
            try
            {

                if (!ModelState.IsValid)
                {
                    returnData.returnCode = -1;
                    returnData.returnMessage = "Dữ liệu đầu vào không hợp lệ";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }

                // kiểm tra xem dữ liệu hợp lệ không 
                if (product == null
                    || product.Id < 0
                    || string.IsNullOrEmpty(product.Name))
                {
                    returnData.returnCode = -1;
                    returnData.returnMessage = "Dữ liệu đầu vào không hợp lệ";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }

                // kiểm tra xem dữ liệu có an toàn không ?
                if (!MyShop.Common.Security.CheckXSSInput(product.Name))
                {
                    returnData.returnCode = -2;
                    returnData.returnMessage = "Tên sản phẩm không hợp lệ";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }

                var rs = new DataAccess.ProductNetFrameWork.DAOImpl.ProductDAOImpl().ProductInsertUpdate(product);

                return Json(rs, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                returnData.returnCode = -999;
                returnData.returnMessage = "Hệ thống đang bận";
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }


        }


        [HttpPost]
        public JsonResult ProductDelete(ProductDeleteRequestData requestData)
        {
            var returnData = new ReturnData();
            try
            {
                var rs = new DataAccess.ProductNetFrameWork.DAOImpl.ProductDAOImpl().ProductDelete(requestData);

                return Json(rs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                returnData.returnCode = -999;
                returnData.returnMessage = "Hệ thống đang bận";
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
        }
    }
}