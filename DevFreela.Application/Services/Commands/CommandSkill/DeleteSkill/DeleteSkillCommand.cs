using DevFreela.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Commands.CommandSkill.DeleteSkill
{
    public class DeleteSkillCommand : IRequest<ResultViewModel>
    {
        public int Id { get; set; }

        public DeleteSkillCommand(int id)
        {
            Id = id;
        }
    }
}
