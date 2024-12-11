using DevFreela.Application.Models;
using DevFreela.Application.Models.Skills;
using DevFreela.Infrastructure.Models.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.SkillServices
{
    public interface ISkillService
    {
        ResultViewModel<int> Insert(CreateSkillInputModel model);
        ResultViewModel<List<SkillViewModel>> GetAll();
        public ResultViewModel<SkillViewModel> GetById(int id);

    }
}
