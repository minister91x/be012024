namespace Eshop.API.Models
{
    public class ReturnData
    {
    }

    public class UserLoginReturnData
    {
        public string userName { get; set; }
        public string token { get; set; }

        public string refeshToken { get; set; }
    }
}
