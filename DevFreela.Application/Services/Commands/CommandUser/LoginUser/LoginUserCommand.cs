using DevFreela.Application.Models;
using DevFreela.Application.Models.user;
using MediatR;

namespace DevFreela.Application.Services.Commands.CommandUser.LoginUser
{
    public class LoginUserCommand:IRequest<ResultViewModel<LoginUserViewModel>>
    {
        public LoginUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
