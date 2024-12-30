using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Commands.CommandUser.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;

        public UpdateUserHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _context.Users.SingleOrDefaultAsync(u => u.Id == request.UserId);
            if (user == null) return ResultViewModel.Error("Usuário Inexistente ou Id Incorreto");
            user.Update(request.FullName,request.Email,request.BirthDate,request.Password,request.Password);
            _context.Users.Update(user);
            _context.SaveChanges();
            return ResultViewModel.Success();
        }
    }
}
