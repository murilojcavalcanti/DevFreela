using Microsoft.AspNetCore.Mvc;
using MediatR;
using DevFreela.Application.Services.Queries.QueriesProject.GetAllProjects;
using DevFreela.Application.Services.Queries.QueriesProject.GetbyIdProjects;
using DevFreela.Application.Services.Commands.CommandSkill.InsertSkill;
using DevFreela.Application.Services.Commands.CommandSkill.DeleteSkill;
using DevFreela.Application.Models.Skills;
using DevFreela.Application.Services.Queries.QueriesSkills.GetAllSkils;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using DevFreela.Application.Services.Queries.QueriesSkills.GetByIdSkill;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    [Authorize]
    public class SkillsControllers:ControllerBase
    {
        private readonly IMediator _mediator;

        public SkillsControllers(IMediator mediator)
        {
            _mediator = mediator;
        }

        //GET api/skills
        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var query = new GetAllSkillQuery();
            var  result = _mediator.Send(query);
            return Ok(result);
        }
        //GET api/skills/{id}
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            var query = new GetByIdSkillQuery(id);
            var result = _mediator.Send(query);
            return Ok(result);
        }
        //POST api/skills
        [HttpPost]
        [Authorize(Roles ="freelacer")]
        public IActionResult Post(InsertSkillCommand Command)
        {
            var result = _mediator.Send(Command);
            SkillViewModel viewModel = SkillViewModel.FromEntity(Command.ToEntity());
            return CreatedAtAction(nameof(GetById), new { id=result.Id}, viewModel);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles ="adm")]
        public IActionResult DeleteSkill(DeleteSkillCommand command)
        {
            var result = _mediator.Send(command);
            return Ok(result);
        }
    }
}
