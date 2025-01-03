﻿using DevFreela.Infrastructure.Models.Skills;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using DevFreela.Application.Services.SkillServices;
using MediatR;
using DevFreela.Application.Services.Queries.QueriesProject.GetAllProjects;
using DevFreela.Application.Services.Queries.QueriesProject.GetbyIdProjects;
using DevFreela.Application.Services.Commands.CommandSkill.InsertSkill;
using DevFreela.Application.Services.Commands.CommandSkill.DeleteSkill;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsControllers:ControllerBase
    {
        private readonly SkillService _service;
        private readonly IMediator _mediator;

        public SkillsControllers(IMediator mediator)
        {
            _mediator = mediator;
        }

        //GET api/skills
        [HttpGet]
        public IActionResult GetAll(string search, int size, int page)
        {
            var query = new GetAllProjectQuery(search,size,page);
            var  result = _mediator.Send(query);
            return Ok(result);
        }
        //GET api/skills/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var query = new GetbyIdQuery(id);
            var result = _mediator.Send(query);
            return Ok(result);
        }
        //POST api/skills
        [HttpPost]
        public IActionResult Post(InsertSkillCommand Command)
        {
            var result = _mediator.Send(Command);
            return CreatedAtAction(nameof(GetById), new { id=result.Id}, result);
        }

        [HttpPost]
        public IActionResult DeleteSkill(DeleteSkillCommand command)
        {
            var result = _mediator.Send(command);
            return Ok(result);

        }

    }
}
