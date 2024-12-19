using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Services.Commands.CommandUser.InsertUserSkill
{
    public class InsertUserSkillCommand:IRequest<ResultViewModel>
    {
        public int Id { get; set; }
        public List<int> SkillsId { get; set; }

        public InsertUserSkillCommand(List<int> skillsId, int id)
        {
            SkillsId = skillsId;
            Id = id;
        }
    }
}
