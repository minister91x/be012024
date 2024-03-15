using DataAccess.Eshop.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Eshop.UnitOfWork
{
    public interface IEShopUnitOfWork
    {
        public IProductRepository _productRepository { get; set; }
        //public IOrderRepository _orderRepository { get; set; }

        public IUseRepository  _useRepository { get; set; }
        void SaveChange();
    }
}
