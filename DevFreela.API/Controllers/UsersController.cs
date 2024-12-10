
using DevFreela.Infrastructure.Models.Skills;
using DevFreela.Infrastructure.Models.user;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers
{
    [Controller]
    [Route("api/users")]
    public class UsersController:ControllerBase
    {
        private readonly DevFreelaDbContext _context;

        public UsersController(DevFreelaDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post(CreateUserInputModel model)
        {
            User user = model.ToEntity();
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _context.Users
                .Include(u => u.Skills)
                .ThenInclude(u => u.Skill).ToList();
            
            return Ok(users);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
           var user = _context.Users
                .Include(u=>u.Skills)
                .ThenInclude(u=>u.Skill)
                .SingleOrDefault(u=>u.Id==id);
            return Ok(user);
        }

        [HttpPost("{id}/skills")]
        public IActionResult PostSkills(int id, UserSkillsInputModel model)
        {
            var UserSkills = model.SkillIds.Select(s => new UserSkill(id, s)).ToList();
            _context.UserSkills.AddRange(UserSkills);
            return Ok();
        }
    }
}
