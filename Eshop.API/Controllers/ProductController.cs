using DataAccess.Eshop.Entities;
using DataAccess.Eshop.IServices;
using DataAccess.Eshop.RequestData;
using DataAccess.Eshop.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        private IEShopUnitOfWork  _unitOfWork;
        public ProductController(IEShopUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Product_Getlist")]
        public async Task<ActionResult> Product_Getlist(GetListProductRequestData requestData)
        {
            var list = new List<Product>();
            list = await _unitOfWork._productRepository.GetProducts(requestData);
            return Ok(list);
        }


        [HttpPost("Product_Delete")]
        public async Task<ActionResult> Product_Delete(ProductDeleteRequestData requestData)
        {
            var list = new ReturnData();
            list = await _unitOfWork._productRepository.Product_Delete(requestData);
            return Ok(list);
        }
    }
}
