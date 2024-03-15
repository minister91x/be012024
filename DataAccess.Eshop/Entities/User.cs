using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Eshop.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string FullName { get; set; }
        public int IsAdmin { get; set; }

        public string RefreshToken { get; set; }
        public DateTime TokenExpriedDate { get; set; }
    }
}
