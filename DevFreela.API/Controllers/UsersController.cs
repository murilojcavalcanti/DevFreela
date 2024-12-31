
using DevFreela.Infrastructure.Models.Skills;
using DevFreela.Infrastructure.Models.user;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevFreela.Application.Services.UserServices;
using MediatR;
using DevFreela.Application.Services.Commands.CommandUser.InsertUser;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using DevFreela.Application.Services.Commands.CommandUser.InsertUserSkill;
using DevFreela.Application.Services.Commands.CommandUser.UpdateUser;
using DevFreela.Application.Services.Commands.CommandUser.DeleteUser;
using DevFreela.Application.Services.Commands.CommandUser.DeleteUserSkill;
using DevFreela.Application.Services.Queries.QueriesUser.GetByIdUser;
using DevFreela.Application.Services.Queries.QueriesProject.GetAllProjects;
using DevFreela.Application.Services.Queries.QueriesUser.GetAllUsers;
using DevFreela.Application.Services.Commands.CommandUser.LoginUser;
namespace DevFreela.API.Controllers
{
    [Controller]
    [Route("api/users")]
    public class UsersController:ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(InsertUserCommand command)
        {
            var result = await _mediator.Send(command);
            if(!result.IsSuccess) return BadRequest(result.Data);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllUsersQuery();
            var result = await _mediator.Send(query);
            if (!result.IsSuccess) return NotFound(result.Data);
            return Ok(result);
        }
        [HttpGet("{id}")]
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
        public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(DeleteUserCommand command)
        {
            var result = await _mediator.Send(command); 
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }
        
        [HttpDelete("DeleteUserSkill")]
        public async Task<IActionResult> DeleteUserSkill(DeleteUserSkillCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }
    }
}
