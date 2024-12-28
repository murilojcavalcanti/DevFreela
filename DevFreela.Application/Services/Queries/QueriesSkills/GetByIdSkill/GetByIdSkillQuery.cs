using DevFreela.Application.Models;
using DevFreela.Application.Models.Skills;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Queries.QueriesSkills.GetByIdSkill
{
    public class GetByIdSkillQuery: IRequest<ResultViewModel<SkillViewModel>>
    {
        public int id { get; set; }

        public GetByIdSkillQuery(int id)
        {
            this.id = id;
        }
    }
}
