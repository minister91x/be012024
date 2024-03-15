using DataAccess.Eshop.RequestData;
using DataAccess.Eshop.UnitOfWork;
using Eshop.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Eshop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IEShopUnitOfWork _unitOfWork;
        private IConfiguration _configuration;
        public AccountController(IEShopUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        [HttpPost("Account_Login")]
        public async Task<ActionResult> Account_Login(UserLoginRequestData requestData)
        {
            var returnData = new UserLoginReturnData();
            try
            {
                if (requestData == null
                    || string.IsNullOrEmpty(requestData.username)
                    || string.IsNullOrEmpty(requestData.password))
                {
                    return Ok(new { msg = "thông tin tài khoản chưa hợp lệ" });
                }
                // Bước 1: gửi userName + password lên server thực hiện đăng nhập
                var user = await _unitOfWork._useRepository.Login(requestData);

                // Bước 2: Kiểm tra thông tin

                //Bước 2.1 nếu không tồn tại
                if (user == null || user.UserId <= 0)
                {
                    return Ok(new { msg = "thông tin tài khoản không đúng" });
                }
                // Bước 2.2 có tồn tại -> trả về token
                var authClaims = new List<Claim> {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()), };

               
                var newAccessToken = CreateToken(authClaims);

                var token = new JwtSecurityTokenHandler().WriteToken(newAccessToken);
                var refreshToken = GenerateRefreshToken();

                // Cập nhật lại refeshtoken vào db
                var expriredDateSettingDay = _configuration["JWT:RefreshTokenValidityInDays"] ?? "";
                var Req = new AccountUpdateRefeshTokenRequestData
                {
                    UserID = user.UserId,
                    RefeshToken = refreshToken,
                    RefeshTokenExpired= DateTime.Now.AddDays(Convert.ToInt32(expriredDateSettingDay))
                };
                var update = await _unitOfWork._useRepository.AccountUpdateRefeshToken(Req);

                returnData.userName = user.UserName;
                returnData.token = token;
                returnData.refeshToken = refreshToken;
                return Ok(returnData);
            }
            catch (Exception ex)
            {

                throw;
            }


            return Ok();
        }
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

    }
}
