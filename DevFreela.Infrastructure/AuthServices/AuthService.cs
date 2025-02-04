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

namespace DevFreela.Infrastructure.AuthServices
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
            var token = new JwtSecurityToken(Issuer,Audience,claims,null,DateTime.Now.AddHours(8),credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
         }

        public  string ComputeHash(string password)
        {
            using (SHA256 hash = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = hash.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes) { 
                    builder.Append(b.ToString("X2"));
                }
                return builder.ToString();
            }
        }
    }
}