using DevFreela.Infrastructure.Models.Skills;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using DevFreela.Application.Services.SkillServices;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsControllers:ControllerBase
    {
        private readonly SkillService _service;

        public SkillsControllers(SkillService service)
        {
            _service = service;
        }

        //GET api/skills
        [HttpGet]
        public IActionResult GetAll()
        {
            var  result = _service.GetAll();
            return Ok(result);
        }
        //GET api/skills/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);
            return Ok(result);
        }
        //POST api/skills
        [HttpPost]
        public IActionResult Post(CreateSkillInputModel model)
        {
            var result = _service.Insert(model);
            return CreatedAtAction(nameof(GetById), new { id=result.Data}, model);
        }


    }
}
