using DevFreela.Application.Models;
using DevFreela.Application.Models.Skills;
using DevFreela.Infrastructure.Models.Skills;
using DevFreela.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.SkillServices
{
    public class SkillService:ISkillService
    {
        private readonly DevFreelaDbContext _context;

        public SkillService(DevFreelaDbContext context)
        {
            _context = context;
        }

        public ResultViewModel<List<SkillViewModel>> GetAll()
        {
            var skills = _context.Skills.ToList();
            var result = skills.Select(s => SkillViewModel.FromEntity(s)).ToList();
            return ResultViewModel<List<SkillViewModel>>.Success(result);
        }
        public ResultViewModel<SkillViewModel> GetById(int id)
        {
            var skill = _context.Skills.FirstOrDefault(s=>s.Id==id);
            if (skill == null) return ResultViewModel<SkillViewModel>.Error("Skill não existe");
            var result = SkillViewModel.FromEntity(skill);
            return ResultViewModel<SkillViewModel>.Success(result);
        }

        public ResultViewModel<int> Insert(CreateSkillInputModel model)
        {
            var skill = model.ToEntity();
            _context.Skills.Add(skill);
            _context.SaveChanges();
            return ResultViewModel<int>.Success(skill.Id);
        }
    }
}
