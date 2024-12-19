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

namespace DevFreela.Application.Services.Commands.CommandUser.DeleteUserSkill
{
    public class DeleteUserSkillHandler: IRequestHandler<DeleteUserSkillCommand,ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;
        public DeleteUserSkillHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel> Handle(DeleteUserSkillCommand request, CancellationToken cancellationToken)
        {
            var userSkil = await _context.UserSkills.SingleOrDefaultAsync(p => p.IdUser == request.UserId && p.IdSkill==request.SkillId);
            if (userSkil is null) return ResultViewModel.Error("UserSkil não existe!");
            userSkil.SetAsDeleted();
            _context.UserSkills.Update(userSkil);
            _context.SaveChangesAsync(true);
            return ResultViewModel.Success();
        }
    }
}
