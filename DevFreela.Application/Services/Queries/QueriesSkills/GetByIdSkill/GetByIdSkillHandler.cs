using DevFreela.Application.Models.Skills;
using DevFreela.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services.Queries.QueriesSkills.GetByIdSkill
{
    public class GetByIdSkillHandler : IRequestHandler<GetByIdSkillQuery, ResultViewModel<SkillViewModel>>
    {
        private readonly DevFreelaDbContext _context;

        public GetByIdSkillHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<SkillViewModel>> Handle(GetByIdSkillQuery request, CancellationToken cancellationToken)
        {
            var skill = await _context.Skills.SingleOrDefaultAsync(s => s.Id == request.id);
            var result = SkillViewModel.FromEntity(skill);
            return ResultViewModel<SkillViewModel>.Success(result);
        }
    }
}
