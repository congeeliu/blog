using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webapi.Models;

namespace webapi.utils
{
    public class JwtHelper
    {
        private readonly IConfiguration _configuration;

        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(User user)
        {
            // 1. 定义需要使用到的Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username), //HttpContext.User.Identity.Name
                //new Claim(ClaimTypes.Role, user.Role), //HttpContext.User.IsInRole("r_admin")
                //new Claim(JwtRegisteredClaimNames.Jti, "admin"),
                new Claim("Id", user.Id.ToString()),
                new Claim("Role", user.Role)
                //new Claim("Name", "超级管理员")
            };

            // 2. 从 appsettings.json 中读取SecretKey
            var key = _configuration["Jwt:SecretKey"];
            key ??= "jwtKey123";
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            // 3. 选择加密算法
            var algorithm = SecurityAlgorithms.HmacSha256;

            // 4. 生成Credentials
            var signingCredentials = new SigningCredentials(secretKey, algorithm);

            // 5. 根据以上，生成token
            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],     //Issuer
                _configuration["Jwt:Audience"],   //Audience
                claims,                          //Claims,
                DateTime.Now,                    //notBefore
                DateTime.Now.AddMinutes(60),    //expires
                signingCredentials               //Credentials
            );

            // 6. 将token变为string
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;
        }
    }
}
