using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Commands.CommandSkill.UpdateSkill
{
    public class UpdateSkillCommand
    {
        public UpdateSkillCommand(int idSkill, string description)
        {
            IdSkill = idSkill;
            Description = description;
        }

        public int IdSkill { get; set; }
        public string Description { get; set; }
    }
}
