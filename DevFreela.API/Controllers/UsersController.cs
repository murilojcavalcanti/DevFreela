using Microsoft.AspNetCore.Mvc;
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
using System.Security.Claims;
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
        [Authorize(Roles ="Adm")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllUsersQuery();
            var result = await _mediator.Send(query);
            if (!result.IsSuccess) return NotFound(result.Data);
            return Ok(result);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Adm")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetByIdUserQuery(id);
            var result = await _mediator.Send(query);
            if (!result.IsSuccess) return NotFound(result.Data);
            return Ok(result);
        }

        [HttpPost("{id}/skills")]
        [Authorize(Roles = "freelancer")]
        public async Task<IActionResult> PostSkills(InsertUserSkillCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            int userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (command.UserId != userId) return Unauthorized("não tem autorização para essa operação");


            var result = await _mediator.Send(command);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserCommand command)
        {
            int userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (command.Id != userId) return Unauthorized("não tem autorização para essa operação");
            
            var result = await _mediator.Send(command); 
            if (!result.IsSuccess) return BadRequest(result.Message);
            
            return Ok(result);
        }
        
        [HttpDelete("{id}/DeleteUserSkill")]
        [Authorize]
        public async Task<IActionResult> DeleteUserSkill([FromBody] DeleteUserSkillCommand command)
        {
            int userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            command.UserId = userId;
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
