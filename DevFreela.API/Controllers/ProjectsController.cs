using DevFreela.API.Entities;
using DevFreela.API.Models.project;
using DevFreela.API.Models.projectComments;
using DevFreela.API.Models.user;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController:ControllerBase
    {
        private readonly DevFreelaDbContext _context;
        public ProjectsController(DevFreelaDbContext context)
        {
            _context = context;
        }
        //GET api/projects?serach=crm
        [HttpGet]
        public IActionResult Get(string search = null)
        {
            List<Project> projects = _context.Projects
                .Include(p=>p.Client)
                .Include(p=>p.Freelancer)
                .Where(p=>!p.IsDeleted).ToList();
            List<ProjectItemViewModel> model = projects.Select(p => ProjectItemViewModel.FromEntity(p)).ToList();
            return Ok(model);
        }

        //GET api/projects/123
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Project? project = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .FirstOrDefault(x => x.Id == id);

            ProjectViewModel model = ProjectViewModel.fromEntity(project);
            return Ok();
        }

        //POST api/projects
        [HttpPost]
        public IActionResult Post(CreateProjectInputModel model)
        {
            var project = model.ToEntity();
            _context.Projects.Add(project);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new {id=1},model);
        }

        // PUT api;projects/123
        [HttpPut("{id}")]
        public IActionResult Put (int id,UpdateProjectInputModel model)
        {
            Project project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if(project is null) return NotFound();
            project.Update(model.Title,model.Description,model.TotalCost);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            Project project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null) return NotFound();
            project.SetAsDeleted();
            _context.Projects.Update(project);
            _context.SaveChanges(true);

            return NoContent();
        }

        //Put api/projest/123/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {

            Project project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null) return NotFound();

            project.Start();
            _context.Projects.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        //PUT api/projects/123/complete
        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            Project project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null) return NotFound();
            project.Complete();
            _context.Projects.Update(project);
            _context.SaveChanges();
            return NoContent();
        }
       
        //Post api/project/123/comments
        [HttpPost("{id}")]
        public IActionResult PostComments(int id, CreateProjectCommentInputModel model)
        {

            Project project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null) return NotFound();
            var comment = new ProjectComment(model.Content,model.IdProject,model.IdUser);
            _context.ProjectComments.Add(comment);
            _context.SaveChanges();
            return Ok();
        }
    }
}
