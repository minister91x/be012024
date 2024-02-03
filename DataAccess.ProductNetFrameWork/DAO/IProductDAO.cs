using DataAccess.ProductNetFrameWork.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ProductNetFrameWork.DAO
{
    public interface IProductDAO
    {
        List<Product> GetProducts(ProductGetListRequestData requestData);
        Product GetProductById(int id);
        ReturnData ProductInsertUpdate(Product product);
        ReturnData ProductDelete(ProductDeleteRequestData requestData);
    }
}
