using DevFreela.Application.Models;
using DevFreela.Application.Models.Skills;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Queries.QueriesSkills.GetAllSkils
{
    public class GetAllUserHandler : IRequestHandler<GetAllSkillQuery, ResultViewModel<List<SkillViewModel>>>
    {
        private readonly DevFreelaDbContext _context;

        public GetAllUserHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<List<SkillViewModel>>> Handle(GetAllSkillQuery request, CancellationToken cancellationToken)
        {
            var skils = _context.Skills.ToList();
            var result = skils.Select(s => SkillViewModel.FromEntity(s)).ToList();

            return ResultViewModel<List<SkillViewModel>>.Success(result);
        }
    }
}
