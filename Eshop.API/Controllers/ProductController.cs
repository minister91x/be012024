using Eshop.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        [HttpGet("Product_Getlist")]
        public async Task<ActionResult> Product_Getlist()
        {
            var list = new List<Product>();
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    list.Add(new Product
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Name " + i,
                        Price = 100 * i
                    });
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return Ok(list);
        }


    }
}
