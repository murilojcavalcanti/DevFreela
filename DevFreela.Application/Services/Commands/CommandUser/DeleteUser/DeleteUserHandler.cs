using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Commands.CommandUser.DeleteUser
{
    public class DeleteUserHandler:IRequestHandler<DeleteUserCommand,ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;
        public DeleteUserHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = _context.Users.FirstOrDefault(u=>u.Id==request.Id);
            if (user is null) return ResultViewModel.Error("Usuario não existente!");
            user.SetAsDeleted();
            _context.Update(user);
            _context.SaveChanges();
            return ResultViewModel.Success();
        }
    }
}
