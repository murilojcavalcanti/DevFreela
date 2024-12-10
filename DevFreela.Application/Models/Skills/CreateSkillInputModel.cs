using DevFreela.Core.Entities;

namespace DevFreela.Infrastructure.Models.Skills
{
    public class CreateSkillInputModel
    {
        public string description { get; set; }
        public Skill ToEntity() => new Skill(description); 
    }
 }

