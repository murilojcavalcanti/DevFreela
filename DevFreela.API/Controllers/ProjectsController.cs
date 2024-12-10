using DevFreela.Infrastructure.Models.projectComments;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevFreela.Application.Models.project;
using DevFreela.Application.Services;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController:ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        //GET api/projects?serach=crm
        [HttpGet]
        public IActionResult GetAll(string search = "",int page =0,int size=1)
        {
            var result= _projectService.GetAll(search,page,size);
            if (!result.IsSuccess) return BadRequest(result.Message); 
            return Ok(result);
        }

        //GET api/projects/123
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _projectService.GetById(id);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }

        //POST api/projects
        [HttpPost]
        public IActionResult Post(CreateProjectInputModel model)
        {
            var result = _projectService.Insert(model);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetById), new {id=result.Data},model);
        }

        // PUT api;projects/123
        [HttpPut("{id}")]
        public IActionResult Put (int id,UpdateProjectInputModel model)
        {
            var result = _projectService.Update(model);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var result = _projectService.Delete(id);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }

        //Put api/projest/123/start
        [HttpPut("start/{id}")]
        public IActionResult Start(int id)
        {

            var result = _projectService.Start(id);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }

        //PUT api/projects/123/complete
        [HttpPut("complete/{id}")]
        public IActionResult Complete(int id)
        {
            var result = _projectService.Complete(id);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }
       
        //Post api/project/123/comments
        [HttpPost("comments/{id}")]
        public IActionResult PostComments(int id, CreateProjectCommentInputModel model)
        {
            var result = _projectService.InsertComment(id,model);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }
    }
}
