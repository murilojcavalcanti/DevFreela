using DevFreela.Core.Services.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string email, string Role)
        {
            var Issuer = _configuration["Jwt:Issuer"];  
            var Audience = _configuration["Jwt:Audience"];
            var Key = _configuration["Jwt:Key"];
            var Skey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var credentials = new SigningCredentials(Skey, SecurityAlgorithms.HmacSha256);
            var claims =new List<Claim>
            {
                new Claim("UserName", email),
                new Claim(ClaimTypes.Role,Role)
            };
            var token = new JwtSecurityToken(issuer: Issuer, audience: Audience, expires:DateTime.Now.AddHours(8),signingCredentials:credentials,claims:claims);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
         }

        string ComputeSha256Hash(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("X2"));
                    
                }
           
                return builder.ToString();
            }
        }
    }
}