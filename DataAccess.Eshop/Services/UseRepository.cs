using DataAccess.Eshop.Entities;
using DataAccess.Eshop.EntitiesFrameWork;
using DataAccess.Eshop.IServices;
using DataAccess.Eshop.RequestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Eshop.Services
{
    public class UseRepository : IUseRepository
    {
        private EshopDBContext _context;
        //public UseRepository(EshopDBContext context)
        //{
        //    _context = context;
        //}

        //public async Task<int> AccountUpdateRefeshToken(AccountUpdateRefeshTokenRequestData requestData)
        //{
        //    try
        //    {
        //        var user = _context.user.Where(s => s.UserId == requestData.UserID).FirstOrDefault();
        //        if (user != null)
        //        {
        //            user.RefreshToken = requestData.RefeshToken;
        //            user.TokenExpriedDate = requestData.RefeshTokenExpired;
        //            _context.user.Update(user);
        //            _context.SaveChanges();

        //            return 1;
        //        }

        //        return 0;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public  async Task<Function> GetFunctionByCode(string FunctionCode)
        //{
        //    return _context.function.Where(s=>s.FunctionCode== FunctionCode).FirstOrDefault();
        //}

        //public async Task<User> Login(UserLoginRequestData requestData)
        //{
        //    try
        //    {
        //        var user = _context.user.ToList().Where(s => s.UserName == requestData.username
        //        && s.PassWord == requestData.password).FirstOrDefault();

        //        return user;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public async Task<UserFunction> UserFunction_GetRole(int UserID, int FunctionId)
        //{
        //    return _context.userfunction.Where(s => s.UserID == UserID && s.FunctionID == FunctionId).FirstOrDefault();
        //}
    }
}
