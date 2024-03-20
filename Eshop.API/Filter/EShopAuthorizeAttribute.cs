using DataAccess.Eshop.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Security.Claims;

namespace Eshop.API.Filter
{
    public class EShopAuthorizeAttribute : TypeFilterAttribute
    {

        public EShopAuthorizeAttribute(string functionCode, string permission) : base(typeof(EShopAuthorizeActionFilter))
        {
            Arguments = new object[] { functionCode, permission };
        }
    }

    public class EShopAuthorizeActionFilter : IAsyncAuthorizationFilter
    {
        private readonly string _functionCode;
        private readonly string _permission;
        private IEShopUnitOfWork _unitOfWork;

        public EShopAuthorizeActionFilter(string functionCode, string permission, IEShopUnitOfWork unitOfWork)
        {
            _functionCode = functionCode;
            _permission = permission;
            _unitOfWork = unitOfWork;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var identity = context.HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                if (userClaims.Count() > 0)
                {
                    var userId = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                    if (userId == null || userId == "")
                    {
                        context.HttpContext.Response.ContentType = "application/json";
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Result = new JsonResult(new
                        {
                            Code = HttpStatusCode.Unauthorized,
                            Message = "Vui lòng đăng nhập để thực hiện chức năng này "
                        });

                        return;
                    }

                    // check quyền
                    var function = await _unitOfWork._useRepository.GetFunctionByCode(_functionCode);
                    if (function == null || function.FunctionID <= 0)
                    {
                        context.HttpContext.Response.ContentType = "application/json";
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Result = new JsonResult(new
                        {
                            Code = HttpStatusCode.Unauthorized,
                            Message = "function không tồn tại"
                        });

                        return;
                    }

                    // lấy quyền
                    var userfunction = await _unitOfWork._useRepository.UserFunction_GetRole(Convert.ToInt32(userId), function.FunctionID);
                    if (userfunction == null || userfunction.UserFunctionID <= 0)
                    {
                        context.HttpContext.Response.ContentType = "application/json";
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Result = new JsonResult(new
                        {
                            Code = HttpStatusCode.Unauthorized,
                            Message = "function không tồn tại"
                        });

                        return;
                    }


                    // 

                    if (_permission == "VIEW")
                    {
                        if (userfunction.IsView == 0)
                        {
                            context.HttpContext.Response.ContentType = "application/json";
                            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            context.Result = new JsonResult(new
                            {
                                Code = HttpStatusCode.Unauthorized,
                                Message = "Bạn không có quyền thực hiện chức năng này"
                            });

                            return;
                        }
                    }
                }
                else
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Result = new JsonResult(new
                    {
                        Code = HttpStatusCode.Unauthorized,
                        Message = "Vui lòng đăng nhập để thực hiện chức năng này "
                    });

                    return;
                }
            }


        }
    }
}
