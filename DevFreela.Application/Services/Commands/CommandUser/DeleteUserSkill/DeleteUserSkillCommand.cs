using DevFreela.Application.Models;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Commands.CommandUser.DeleteUserSkill
{
    public class DeleteUserSkillCommand:IRequest<ResultViewModel>
    {
        public DeleteUserSkillCommand(int userId, int skillId)
        {
            UserId = userId;
            SkillId = skillId;
        }

        public int UserId { get; set; }
        public int SkillId { get; set; }
    }
}
