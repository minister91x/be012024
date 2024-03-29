using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMVC.Models
{
    public class ReturnData
    {
        public int ReturnCode { get; set; }
        public int ReturnMessage { get; set; }
    }

    public class LoginResponseData : ReturnData
    {
        public string userName { get; set; }
        public string token { get; set; }
        public string refeshToken { get; set; }

        public int isAdmin { get; set; }
    }
}