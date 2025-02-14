using DevFreela.Application.Models;
using DevFreela.Application.Models.user;
using DevFreela.Application.Services.Queries.QueriesUser.GetByEmailUser;
using DevFreela.Application.Services.Queries.QueriesUser.GetByIdUser;
using DevFreela.Core.Entities;
using DevFreela.Core.Services.Auth;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Commands.CommandUser.LoginUser
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, ResultViewModel<loginViewModel>>
    {
        private readonly IAuthService _authService;
        private readonly DevFreelaDbContext _context;
        public LoginUserHandler(IAuthService authService, DevFreelaDbContext context)
        {
            _authService = authService;
            _context = context;
        }

        public async Task<ResultViewModel<loginViewModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            User? user = _context.Users.FirstOrDefault(u=>u.Email ==request.Email);
            string passwordHash = _authService.ComputeHash(request.Password);

            if (user == null|| user.Password != passwordHash) return ResultViewModel<loginViewModel>.Error("Email ou senha incorretos");
            
            var token = _authService.GenerateToken(user.Email,user.Id, user.Role);
            var loginUserViewModel = new loginViewModel(token);
            
            return ResultViewModel<loginViewModel>.Success(loginUserViewModel);
        }
    }
}
