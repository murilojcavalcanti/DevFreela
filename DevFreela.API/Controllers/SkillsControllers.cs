using DevFreela.Infrastructure.Models.Skills;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsControllers:ControllerBase
    {
        private readonly DevFreelaDbContext _context;

        public SkillsControllers(DevFreelaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var skills = _context.Skills.ToList();
            return Ok(skills);
        }
        //POST api/skills
        [HttpPost]
        public IActionResult Post(CreateSkillInputModel model)
        {
            Skill skill = new Skill(model.description);
            _context.Skills.Add(skill);
            _context.SaveChanges();
            return Created();
        }


    }
}
