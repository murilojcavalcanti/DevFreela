using DevFreela.Infrastructure.Models.projectComments;
using Microsoft.AspNetCore.Mvc;
using DevFreela.Application.Models.project;
using DevFreela.Application.Services.ProjectServices;
using MediatR;
using DevFreela.Application.Services.Queries.QueriesProject.GetAllProjects;
using DevFreela.Application.Services.Queries.QueriesProject.GetbyIdProjects;
using DevFreela.Application.Services.Commands.CommandsProject.InsertProject;
using DevFreela.Application.Services.Commands.CommandsProject.UpdateProject;
using DevFreela.Application.Services.Commands.CommandsProject.DeleteProject;
using DevFreela.Application.Services.Commands.CommandsProject.StartProject;
using DevFreela.Application.Services.Commands.CommandsProject.CompleteProject;
using DevFreela.Application.Services.Commands.CommandsProject.InsertCommentProject;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController:ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        //GET api/projects?serach=crm
        [HttpGet]
        public async Task<IActionResult> GetAll(string search = "",int page =0,int size=5)
        {
            var query = new GetAllProjectQuery(search,size,page);
            var result = await _mediator.Send(query);
            if (!result.IsSuccess) return BadRequest(result.Message); 
            return Ok(result);
        }

        //GET api/projects/123
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetbyIdQuery(id);
            var result = await _mediator.Send(query);
            //if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }

        //POST api/projects
        [HttpPost]
        public async Task<IActionResult> Post(InsertProjectCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetById), new {id=result.Data},command);
        }

        // PUT api;projects/123
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync (UpdateProjectCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(DeleteProjectCommand command)
        {

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        //Put api/projest/123/start
        [HttpPut("start/{id}")]
        public async Task<IActionResult> Start(StartProjectCommand command)
        {

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        //PUT api/projects/123/complete
        [HttpPut("complete/{id}")]
        public  async Task<IActionResult> Complete(CompleteProjectCommand command)
        {
            var result = _mediator.Send(command);
            return Ok(result);
        }
       
        //Post api/project/123/comments
        [HttpPost("comments/{id}")]
        public async Task<IActionResult> PostComments(InsertCommentProjectCommand command)
        {
            var result =await _mediator.Send(command);
            return Ok(result);
        }
    }
}
