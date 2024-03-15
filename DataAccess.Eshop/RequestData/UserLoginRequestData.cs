using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Eshop.RequestData
{
    public class UserLoginRequestData
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class AccountUpdateRefeshTokenRequestData
    {
        public int UserID { get; set; }
        public string RefeshToken { get; set; }

        public DateTime RefeshTokenExpired { get; set; }
    }
}
