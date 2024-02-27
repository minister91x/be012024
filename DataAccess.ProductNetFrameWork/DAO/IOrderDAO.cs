using DataAccess.ProductNetFrameWork.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ProductNetFrameWork.DAO
{
    public interface IOrderDAO
    {
        int OrderCreate(Order order);
        List<Order> GetListOrder(string MaDonHang, DateTime fromDate, DateTime toDate);
    }
}
