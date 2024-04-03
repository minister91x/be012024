using Dapper;
using DataAccess.Eshop.Dapper;
using DataAccess.Eshop.Entities;
using DataAccess.Eshop.IServices;
using DataAccess.Eshop.RequestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Eshop.Services
{
    public class ProductDapperRepositiry : BaseApplicationService, IProductDapperRepository
    {
        public ProductDapperRepositiry(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public Task<Product> GetProductById(GetProductByIdRequestData requestData)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetProducts(GetListProductRequestData requestData)
        {
            var list = new List<Product>();
            try
            {
                var param = new DynamicParameters();
                param.Add("@_ProductID", requestData.ProductId);
                list = await DbConnection.QueryAsync<Product>("SP_Product_GetList", param);
            }
            catch (Exception exx)
            {

                throw;
            }

            return list;
        }

        public Task<ReturnData> Product_Delete(ProductDeleteRequestData requestData)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnData> Product_InsertUpdate(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
