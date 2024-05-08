using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ProductNetFrameWork.DTO
{
    // Domain model
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được trống")]
        public string Name { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }

        public string ImageSrc { get; set; }

        public string AttPrice { get; set; }
        public string AttrQuantity { get; set; }
    }

    public class ProductFromAPI
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public int categoryID { get; set; }
        public int price { get; set; }
    }

    // View Model

    public class Product_ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }


    public class ProductGetListRequestData
    {
        public string ProductName { get; set; }
    }

    public class ProductDeleteRequestData
    {
        public int Id { get; set; }
    }
}
