using DevFreela.Application.Models;
using DevFreela.Application.Models.user;
using DevFreela.Application.Services.Queries.QueriesUser.GetByEmailUser;
using DevFreela.Application.Services.Queries.QueriesUser.GetByIdUser;
using DevFreela.Core.Services.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Commands.CommandUser.LoginUser
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, ResultViewModel<LoginUserViewModel>>
    {
        private readonly IAuthService _authService;
        private readonly IMediator _mediator;
        public LoginUserHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<ResultViewModel<LoginUserViewModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var query = new GetByEmailUserQuery(request.Email);
            var user = _mediator.Send(query);
            var passwordHash = _authService.ComputeSha256Hash(request.Password);
            if (user.Result.Data == null|| user.Result.Data.Password != passwordHash) return ResultViewModel<LoginUserViewModel>.Error("Email ou senha incorretos");
            var token = _authService.GenerateToken(user.Result.Data.Email, user.Result.Data.Role);
            var loginUserViewModel = new LoginUserViewModel(user.Result.Data.Email, token);
            return ResultViewModel<LoginUserViewModel>.Success(loginUserViewModel);
        }
    }
}
