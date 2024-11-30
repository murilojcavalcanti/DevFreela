using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [Route("api/porjects")]
    [ApiController]
    public class ProjectsController:ControllerBase
    {
        private readonly FreelanceTotalCostConfig _config
        public ProjectsController(IOptions<FreelanceTotalCostConfig> opts)
        {
            _config = opts.Value;
            
        }
        //GET api/projects?serach=crm
        [HttpGet]
        public IActionResult Get(string search)
        {
            return Ok();
        }

        //GET api/projects/123
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        //POST api/projects
        [HttpPost]
        public IActionResult Post(CreateInputModel model)
        {
            if (model.TotalCost < _config.Minimum || model.TotalCost > _config.Maxmum)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new {id=1},model);
        }

        // PUT api;projects/123
        [HttpPut("{id}")]
        public IActionResult Put (int id,UpdateProjectInputModel model)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }

        //Put api/projest/123/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int Id)
        {
            return NoContent();
        }

        //PUT api/projects/123/complete
        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            return NoContent();
        }
        // PUT api;projects/123/cover
        [HttpPut("{id}/profile")]
        public IActionResult PostProfile(int id, IFormFile file)
        {

            return NoContent();
        }

        //Post api/project/123/comments
        [HttpPost("{id}")]
        public IActionResult PostComments(int id, CreateProjectCommentInputModel model)
        {
            return Ok();
        }
    }
}
