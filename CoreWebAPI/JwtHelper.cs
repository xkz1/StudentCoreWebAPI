using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.IdentityModel.Tokens;

using Model.Entity;
namespace DemoJWT.Authorization
{
    public class JwtHelper
    {
        static IConfiguration _configuration;

        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        static UserEntity user = new UserEntity();
        public static string GenerateJsonWebToken(UserEntity userInfo)
        {
            var signingAlgorithm = SecurityAlgorithms.HmacSha256;

            Claim[]? claims = new[]
 {
   new Claim(JwtRegisteredClaimNames.Sub,userInfo.UserAccount),
    new Claim(ClaimTypes.Role,userInfo.UserName),
    new Claim("UserId",userInfo.Id.ToString()),
};

            var secretByte = Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]);
            var signingKey = new SymmetricSecurityKey(secretByte);
            var signingCredentials = new SigningCredentials(signingKey, signingAlgorithm);

            var token = new JwtSecurityToken(
                issuer: _configuration["Authentication:Issuer"],
                audience: _configuration["Authentication:Audience"],
                claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials
            );

            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenStr;

        }

        public static class TokenParameter
        {
            public const string Issuer = "zzg";//颁发者
            public static string Audience = user.UserAccount;//接收者
            public const string Secret = "364758";//签名秘钥
            public const int AccessExpiration = 1440;//AccessToken过期时间（分钟） 1440 1天
        }

    }
}