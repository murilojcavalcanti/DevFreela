using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Services.Auth
{
    public interface IAuthService
    {
        string GenerateToken(string email, string Role);
    }
}
