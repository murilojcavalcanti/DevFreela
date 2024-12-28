using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Commands.CommandSkill.InsertSkill
{
    public class InsertSkillHandler : IRequestHandler<InsertSkillCommand, ResultViewModel<int>>
    {
        private DevFreelaDbContext _context;

        public InsertSkillHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<int>> Handle(InsertSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = request.ToEntity();
            _context.Skills.Add(skill);
            _context.SaveChanges();
            return ResultViewModel<int>.Success(skill.Id);
            
        }
    }
}
