using DataAccess.Eshop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Eshop.IServices
{
    public interface IProductServices
    {
        Task<List<Product>> GetProducts();

        Task<Product> GetProductById(GetProductByIdRequestData requestData);

        Task<ReturnData> Product_InsertUpdate(Product product);
        Task<ReturnData> Product_Delete(ProductDeleteRequestData requestData);
            
    }
}
