using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ProductNetFrameWork.DTO
{
    public class Order
    {
        public int OrderID { get; set; }
        public string MaDonHang { get; set; }
        public int CustomerID { get; set; }
        public int TotalAmount { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }

        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
    }
}
