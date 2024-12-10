
using DevFreela.Infrastructure.Models.Skills;
using DevFreela.Infrastructure.Models.user;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevFreela.Application.Services.UserServices;
namespace DevFreela.API.Controllers
{
    [Controller]
    [Route("api/users")]
    public class UsersController:ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Post(CreateUserInputModel model)
        {
            var result = _service.Insert(model);
            if(!result.IsSuccess) return BadRequest(result.Data);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
        }

        [HttpGet]
        public IActionResult GetAll(string search = "", int size = 5, int page =0 )
        {
            var result = _service.GetAll(search,size,page);
            if (!result.IsSuccess) return NotFound(result.Data);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
           var result = _service.GetById(id);
            if (!result.IsSuccess) return NotFound(result.Data);
            return Ok(result);
        }

        [HttpPost("{id}/skills")]
        public IActionResult PostSkills(int id, UserSkillsInputModel model)
        {
            var result = _service.InsertSkills(id,model);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result);
        }
    }
}
