using DevFreela.Application.Models;
using DevFreela.Application.Models.Skills;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Queries.QueriesSkills.GetAllSkils
{
    public class GetAllSkillQuery : IRequest<ResultViewModel<List<SkillViewModel>>
    {
        
    }
}
