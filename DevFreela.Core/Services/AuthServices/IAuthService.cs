using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Services.Auth
{
    public interface IAuthService
    {
        public string GenerateToken(string email, int Id, string Role);
        public string ComputeHash(string password);
    }
}
