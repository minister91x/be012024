using DataAccess.Eshop.EntitiesFrameWork;
using DataAccess.Eshop.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Eshop.UnitOfWork
{
    public class EShopUnitOfWork : IEShopUnitOfWork,IDisposable
    {
        public IProductRepository _productRepository { get; set; }
        public IUseRepository _useRepository { get; set; }

        // public IOrderRepository _orderRepository { get; set; }
        public EshopDBContext _eshopDBContext;

        public EShopUnitOfWork(IProductRepository productRepository, IUseRepository useRepository,  EshopDBContext eshopDBContext)
        {
            _productRepository = productRepository;
           // _orderRepository = orderRepository;
            _eshopDBContext = eshopDBContext;
            _useRepository = useRepository; 
        }


        public async void SaveChange()
        {
            _eshopDBContext.SaveChanges();
        }

        public void Dispose()
        {
            _eshopDBContext.Dispose();
        }
    }
}
