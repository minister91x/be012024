using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ProductNetFrameWork.DTO
{
    public class ShoppingCart
    {
        // giữ phím ALT Và kéo chuột
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quality { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
    }
}
