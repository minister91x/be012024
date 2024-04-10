using DataAccess.Eshop.Entities;
using DataAccess.Eshop.IServices;
using DataAccess.Eshop.RequestData;
using DataAccess.Eshop.UnitOfWork;
using Eshop.API.Filter;
using Eshop.API.LogManager;
using Eshop.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ReturnData = DataAccess.Eshop.Entities.ReturnData;

namespace Eshop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //private IProductRepository _productRepository;
        //public ProductController(IProductRepository productRepository) // TIÊM 
        //{
        //    _productRepository = productRepository;
        //}

        private IEShopUnitOfWork _unitOfWork;
        private readonly IDistributedCache _cache;
        private IConfiguration _configuration;
        private ILoggerManager _loggerManager;
        public ProductController(IEShopUnitOfWork unitOfWork, IDistributedCache cache, 
            IConfiguration configuration,ILoggerManager loggerManager)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
            _configuration = configuration;
            _loggerManager = loggerManager;
        }

        [HttpPost("Product_Getlist")]
        // [EShopAuthorize("Product_Getlist", "VIEW")]
        public async Task<ActionResult> Product_Getlist(GetListProductRequestData requestData)
        {
            var list = new List<Product>();

            string cacheKey = "PRODUCT_GETLIST_CACHING_" + requestData.ProductId;
            // Trying to get data from the Redis cache

            byte[] cachedData = await _cache.GetAsync(cacheKey);

            // Nếu đã có dữ liệu trong cache thì lấy luôn từ cache 

            if (cachedData != null)
            {
                // If the data is found in the cache, encode and deserialize cached data.
                var cachedDataString = Encoding.UTF8.GetString(cachedData);

                list = JsonConvert.DeserializeObject<List<Product>>(cachedDataString);

                _loggerManager.LogError("list product cache: | " + JsonConvert.SerializeObject(list));
                return Ok(list);
            }

            //Nếu trong cache không có dữ liệu thì Vào database để lấy dữ liệu

            list = await _unitOfWork._productDapperRepository.GetProducts(requestData);
            _loggerManager.LogError("list product : | "+ JsonConvert.SerializeObject(list));
            // Set lại dữ liệu vào cache 


            if (list.Count > 0)
            {
                string cachedDataString = JsonConvert.SerializeObject(list);
                var dataToCache = Encoding.UTF8.GetBytes(cachedDataString);
                // Setting up the cache options
                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(1))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(3));
                // Add the data into the cache

                await _cache.SetAsync(cacheKey, dataToCache, options);

            }

            return Ok(list);
        }


        [HttpPost("Product_InsertUpdate")]
        // [EShopAuthorize("Product_Delete", "DELETE")]
        public async Task<ActionResult> Product_InsertUpdate(Product requestData)
        {
            var result = new DataAccess.Eshop.Entities.ReturnData();
            try
            {
                // gọi MEDIA ĐỂ LẤY TÊN ẢNH TỪ BASE64
                var media_url = _configuration["MEDIA:URL"] ?? "http://localhost:55587/";
                var base_url = "api/UploadImage/SaveImage_Data";
                var returnData = new ReturnData();
                if (!string.IsNullOrEmpty(requestData.Base64Image))
                {
                    // kiểm tra xem chữ ký có hợp lệ không ?
                    var SecretKey = _configuration["SecretKey:IMAGE_DOWN_UPLOAD"] ?? "";
                    var plantext = requestData.Base64Image + SecretKey;
                    var Sign = MyShop.Common.Security.MD5(plantext);

                    var req = new SaveImage_DataRequestData
                    {
                        Base64Image = requestData.Base64Image,
                        Sign = Sign
                    };

                    var jsonBody = JsonConvert.SerializeObject(req);
                    var result_media = await MyShop.Common.HttpRequestHelper.HttpClientSend(media_url, base_url, jsonBody);
                    returnData = JsonConvert.DeserializeObject<ReturnData>(result_media);
                    if (returnData.ReturnCode < 0)
                    {
                        result.ReturnCode = -1;
                        result.ReturnMsg = returnData.ReturnMsg;
                        return Ok(result);
                    }

                }

                // gán lại tên ảnh vào Base64Image
                requestData.Base64Image = returnData.ReturnMsg;

                result = await _unitOfWork._productRepository.Product_InsertUpdate(requestData);
            }
            catch (Exception ex)
            {
              
                throw;
            }

            return Ok(result);
        }


        [HttpPost("Product_InsertUpdateByFile")]
        // [EShopAuthorize("Product_Delete", "DELETE")]
        public async Task<ActionResult> Product_InsertUpdateByFile([FromForm] UploadFileInputDto formFile)
        {
            try
            {
                if (formFile == null || formFile.File.Length <= 0)
                {
                    throw new Exception("file dữ liệu không được trống");
                }

                if (!Path.GetExtension(formFile.File.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("Hệ thống chỉ hỗ trợ file .xlsx");
                }

                using (var stream = new MemoryStream())
                {
                    await formFile.File.CopyToAsync(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.Commercial;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var name = worksheet.Cells[row, 1]?.Value?.ToString()?.Trim();
                            var price = worksheet.Cells[row, 2]?.Value?.ToString()?.Trim();

                            await _unitOfWork._productRepository.Product_InsertUpdate(new Product
                            {
                                ProductName = name,
                                Price = Convert.ToInt32(price)
                            });
                        }


                    }

                    _unitOfWork.SaveChange();
                }

                return Ok();

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Ok();
        }





        [HttpPost("Product_Delete")]
        [EShopAuthorize("Product_Delete", "DELETE")]
        public async Task<ActionResult> Product_Delete(ProductDeleteRequestData requestData)
        {
            var list = new ReturnData();
            //  list = await _unitOfWork._productRepository.Product_Delete(requestData);
            return Ok(list);
        }
    }
}
