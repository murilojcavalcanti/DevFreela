﻿using Microsoft.AspNetCore.Mvc;
using MediatR;
using DevFreela.Application.Services.Commands.CommandUser.InsertUser;
using DevFreela.Application.Services.Commands.CommandUser.InsertUserSkill;
using DevFreela.Application.Services.Commands.CommandUser.UpdateUser;
using DevFreela.Application.Services.Commands.CommandUser.DeleteUser;
using DevFreela.Application.Services.Commands.CommandUser.DeleteUserSkill;
using DevFreela.Application.Services.Queries.QueriesUser.GetByIdUser;
using DevFreela.Application.Services.Queries.QueriesUser.GetAllUsers;
using DevFreela.Application.Services.Commands.CommandUser.LoginUser;
using Microsoft.AspNetCore.Authorization;
using DevFreela.Infrastructure.Models.user;
using DevFreela.Core.Entities;
namespace DevFreela.API.Controllers
{
    [Controller]
    [Route("api/users")]
    [Authorize]
    public class UsersController:ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]InsertUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result.Data);
            UserViewModel viewModel = UserViewModel.FromEntity(command.ToEntity());

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllUsersQuery();
            var result = await _mediator.Send(query);
            if (!result.IsSuccess) return NotFound(result.Data);
            return Ok(result);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetByIdUserQuery(id);
           var result = await _mediator.Send(query);
            if (!result.IsSuccess) return NotFound(result.Data);
            return Ok(result);
        }

        [HttpPost("{id}/skills")]
        public async Task<IActionResult> PostSkills(InsertUserSkillCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserCommand command)
        {
            var result = await _mediator.Send(command); 
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }
        
        [HttpDelete("{id}/DeleteUserSkill")]
        public async Task<IActionResult> DeleteUserSkill([FromBody] DeleteUserSkillCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }
    }
}
