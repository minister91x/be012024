using DataAccess.Eshop.EntitiesFrameWork;
using DataAccess.Eshop.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Eshop.Services
{
    public class OrderRepository: IOrderRepository
    {
        private EshopDBContext _context;
        public OrderRepository(EshopDBContext context)
        {
            _context = context;
        }
    }
}
