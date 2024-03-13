using DataAccess.ProductNetFrameWork.DAO;
using DataAccess.ProductNetFrameWork.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ProductNetFrameWork.DAOImpl
{
    public class ProductDAOImpl : IProductDAO
    {
        public Product GetProductById(int id)
        {
            try
            {
                var list = InitialProduct();
                var product = list.Where(s => s.Id == id).FirstOrDefault();
                return product;
            }
            catch (Exception EX)
            {

                throw;
            }
        }

        public List<Product> GetProducts(ProductGetListRequestData requestData)
        {

            try
            {
                if (!string.IsNullOrEmpty(requestData.ProductName))
                {
                    if (!MyShop.Common.Security.CheckXSSInput(requestData.ProductName))
                    {
                        return new List<Product>();
                    }
                }
                var list = InitialProduct();

                list = !string.IsNullOrEmpty(requestData.ProductName)
                    ? list.FindAll(s => s.Name == requestData.ProductName).ToList()
                    : list;

                return list;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public ReturnData ProductDelete(ProductDeleteRequestData requestData)
        {
            var returnData = new ReturnData();
            try
            {
                if (requestData == null || requestData.Id < 0)
                {
                    returnData.returnCode = -1;
                    returnData.returnMessage = "Dữ liệu đầu vào không hợp lệ";
                    return returnData;
                }

                var list = InitialProduct();
                var product = list.Where(s => s.Id == requestData.Id).FirstOrDefault();

                if (product == null || product.Id <= 0)
                {
                    returnData.returnCode = -2;
                    returnData.returnMessage = "Không tồn tại sản phẩm cần xóa";
                    return returnData;
                }

                var result = list.Remove(product);


                returnData.returnCode = 1;
                returnData.returnMessage = "Xóa sản phẩm thành công!";
                return returnData;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ReturnData ProductInsertUpdate(Product product)
        {
            var returnData = new ReturnData();
            try
            {
                // kiểm tra xem dữ liệu hợp lệ không 
                if (product == null
                    || product.Id < 0
                    || string.IsNullOrEmpty(product.Name))
                {
                    returnData.returnCode = -1;
                    returnData.returnMessage = "Dữ liệu đầu vào không hợp lệ";
                    return returnData;
                }

                // kiểm tra xem dữ liệu có an toàn không ?
                if (!MyShop.Common.Security.CheckXSSInput(product.Name))
                {
                    returnData.returnCode = -2;
                    returnData.returnMessage = "Tên sản phẩm không hợp lệ";
                    return returnData;
                }

                var list = InitialProduct();

                if (product.Id <= 0)
                {
                    // Insert 

                    list.Add(product);

                    returnData.returnCode = 1;
                    returnData.returnMessage = "Thêm sản phẩm thành công!";
                    return returnData;
                }
                else
                {
                    //Update 

                    var productUpdate = list.Where(s => s.Id == product.Id).FirstOrDefault();

                    if (product == null || product.Id <= 0)
                    {
                        returnData.returnCode = -2;
                        returnData.returnMessage = "Không tồn tại sản phẩm cần xóa";
                        return returnData;
                    }

                    productUpdate.Name = product.Name;

                    returnData.returnCode = 1;
                    returnData.returnMessage = "Cập nhật sản phẩm thành công!";
                    return returnData;
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private List<Product> InitialProduct()
        {
            var list = new List<Product>();
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    list.Add(new Product { Id = i + 1, Name = "Product " + i, Price = 10000 });
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return list;
        }
    }
}
