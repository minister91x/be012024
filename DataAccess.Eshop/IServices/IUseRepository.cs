using DataAccess.Eshop.Entities;
using DataAccess.Eshop.RequestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Eshop.IServices
{
    public interface IUseRepository
    {
        Task<User> Login(UserLoginRequestData requestData);

        Task<int> AccountUpdateRefeshToken(AccountUpdateRefeshTokenRequestData requestData);
    }
}
