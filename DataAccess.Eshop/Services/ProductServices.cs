﻿using DataAccess.Eshop.Entities;
using DataAccess.Eshop.EntitiesFrameWork;
using DataAccess.Eshop.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Eshop.Services
{
    public class ProductServices : IProductServices
    {
        private EshopDBContext _context;
        public ProductServices(EshopDBContext context)
        {
            _context = context;
        }

        public Task<Product> GetProductById(GetProductByIdRequestData requestData)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetProducts()
        {
            var list = new List<Product>();
            try
            {
                //for (int i = 0; i < 5; i++)
                //{
                //    list.Add(new Product
                //    {
                //        ProductId = i,
                //        ProductName = "Name " + i,
                //        Price = 100 * i
                //    });
                //}

                return _context.Product.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }

            return list;
        }

        public async Task<ReturnData> Product_Delete(ProductDeleteRequestData requestData)
        {
            var returnData = new ReturnData();
            try
            {
                // step 1 : Kiểm tra đầu vào hợp lệ 

                if (requestData == null || requestData.Id <= 0)
                {
                    returnData.ReturnCode = -1;
                    returnData.ReturnMsg = "Dữ liệu đầu vào không hợp lệ";
                    return returnData;
                }

                // step 2 : Kiểm tra dữ liệu tồn tại không

                var product = _context.Product.Where(s => s.ProductId == requestData.Id).FirstOrDefault();
                if (product == null || product.ProductId <= 0)
                {
                    returnData.ReturnCode = -2;
                    returnData.ReturnMsg = "Không tồn tại sản phẩm cần xóa";
                    return returnData;
                }


                // step 3 : Xóa

                _context.Product.Remove(product);

                var result = _context.SaveChanges();

                if (result < 0)
                {
                    returnData.ReturnCode = -11;
                    returnData.ReturnMsg = "Xóa sản phẩm thất bại";
                    return returnData;
                }


                returnData.ReturnCode = 1;
                returnData.ReturnMsg = "Xóa sản phẩm thành công";
                return returnData;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<ReturnData> Product_InsertUpdate(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
