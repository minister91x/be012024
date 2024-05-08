using DataAccess.ProductNetFrameWork.DTO;
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

        // [ValidateAntiForgeryToken]
        public JsonResult Product_InsertUpdate(Product product)
        {
            var returnData = new DataAccess.ProductNetFrameWork.DTO.ReturnData();
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

                if (string.IsNullOrEmpty(product.ImageSrc))
                {
                    returnData.returnCode = -3;
                    returnData.returnMessage = "Sản phẩm không hợp lệ do chưa có ảnh?";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }


                // Gọi API ĐỂ LƯU VÀ LẤY TÊN ẢNH 
                var productImage = "";

                if (!string.IsNullOrEmpty(product.ImageSrc))
                {
                    var SecretKey = System.Configuration.ConfigurationManager.AppSettings["SecretKey"] ?? "";
                    string urlPath = System.Configuration.ConfigurationManager.AppSettings["API_URL"] ?? "";
                    string baseUrl = "SaveImage_Data";


                    var ImageCount = product.ImageSrc.Split('_').Count();
                    for (int i = 0; i < ImageCount; i++)
                    {
                        var productImgBase64 = product.ImageSrc.Split('_')[i];
                        // call sang api
                        //productImage+= 

                        var imageReq = new SaveImage_DataRequestData
                        {
                            Base64Image = productImgBase64,
                            Sign = MyShop.Common.Security.MD5(productImgBase64 + SecretKey)
                        };

                        string jsonBody = JsonConvert.SerializeObject(imageReq);

                        // gọi api

                        var result = MyShop.Common.HttpRequestHelper.SendPost(urlPath, baseUrl, jsonBody);

                        if (!string.IsNullOrEmpty(result))
                        {
                            var SaveImgResp = JsonConvert.DeserializeObject<SaveImageReturnData>(result);
                            if (SaveImgResp.ReturnCode > 0)
                            {
                                productImage += SaveImgResp.ReturnMsg + ",";
                            }
                        }
                    }


                    if (!string.IsNullOrEmpty(productImage))
                    {
                        productImage = productImage.Substring(0, productImage.Length - 1);
                    }

                    product.ImageSrc = productImage;
                }

                
                var rs = new DataAccess.ProductNetFrameWork.DAOImpl.ProductDAOImpl().ProductInsertUpdate(product);

                // xử lý Atrribute 
                var priceATTr = product.AttPrice;


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
            var returnData = new DataAccess.ProductNetFrameWork.DTO.ReturnData();
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