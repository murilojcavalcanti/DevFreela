using DevFreela.API.Models.Skills;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsControllers:ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }
        //POST api/skills
        [HttpPost]
        public IActionResult Post(CreateSkillInputModel model)
        {
            return Ok();
        }


    }
}
