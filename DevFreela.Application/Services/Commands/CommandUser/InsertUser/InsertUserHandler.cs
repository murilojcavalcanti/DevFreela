using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Services.Auth;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Commands.CommandUser.InsertUser
{
    public class InsertUserHandler : IRequestHandler<InsertUserCommand, ResultViewModel<int>>
    {
        private  readonly DevFreelaDbContext _context;
        private readonly IAuthService _authService;
        public InsertUserHandler(DevFreelaDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<ResultViewModel<int>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _authService.ComputeSha256Hash(request.Password);
            User user = request.ToEntity();
            user.Password = passwordHash;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return ResultViewModel<int>.Success(user.Id);
        }
    }
}
