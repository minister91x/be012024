using DataAccess.Eshop.Entities;
using DataAccess.Eshop.IServices;
using DataAccess.Eshop.RequestData;
using DataAccess.Eshop.UnitOfWork;
using Eshop.API.Filter;
using Eshop.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OfficeOpenXml;
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

        public ProductController(IEShopUnitOfWork unitOfWork, IDistributedCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
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

                list = JsonSerializer.Deserialize<List<Product>>(cachedDataString);
                return Ok(list);
            }

            //Nếu trong cache không có dữ liệu thì Vào database để lấy dữ liệu

            list = await _unitOfWork._productDapperRepository.GetProducts(requestData);

            // Set lại dữ liệu vào cache 


            if (list.Count > 0)
            {
                string cachedDataString = JsonSerializer.Serialize(list);
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
