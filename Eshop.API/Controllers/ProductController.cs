using DataAccess.Eshop.Entities;
using DataAccess.Eshop.IServices;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductServices _productServices;
        public ProductController(IProductServices productServices) // TIÊM 
        {
            _productServices = productServices;
        }


        [HttpGet("Product_Getlist")]
        public async Task<ActionResult> Product_Getlist()
        {
            var list = new List<Product>();
            list = await _productServices.GetProducts();
            return Ok(list);
        }


        [HttpPost("Product_Delete")]
        public async Task<ActionResult> Product_Delete(ProductDeleteRequestData requestData)
        {
            var list = new ReturnData();
            list = await _productServices.Product_Delete(requestData);
            return Ok(list);
        }
    }
}
