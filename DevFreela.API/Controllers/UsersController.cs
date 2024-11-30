using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Controller]
    [Route("api/users")]
    public class UsersController:ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }
    }
}
