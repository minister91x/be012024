using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Eshop.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int CategoryID { get; set; }
        public int Price { get; set; }
    }
}
