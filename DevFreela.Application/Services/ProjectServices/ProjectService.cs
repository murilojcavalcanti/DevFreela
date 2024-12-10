using Azure;
using DevFreela.Application.Models;
using DevFreela.Application.Models.project;
using DevFreela.Application.Services.ProjectServices;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Models.projectComments;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services.ProjectServices
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _context;

        public ProjectService(DevFreelaDbContext context)
        {
            _context = context;
        }

        public ResultViewModel Complete(int id)
        {
            Project project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null) return ResultViewModel.Error("Projeto não existe!");
            project.Complete();
            _context.Projects.Update(project);
            _context.SaveChanges();
            return ResultViewModel.Success();
        }

        public ResultViewModel Delete(int id)
        {
            Project project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null) return ResultViewModel.Error("Projeto não existe!");
            project.SetAsDeleted();
            _context.Projects.Update(project);
            _context.SaveChanges(true);
            return ResultViewModel.Success();
        }

        public ResultViewModel<List<ProjectItemViewModel>> GetAll(string search = "",int page = 0, int size = 5)
        {
            List<Project> projects = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Where(p => !p.IsDeleted &&
                (search == "" ||
                p.Title.Contains(search)))
                .Skip(page * size)
                .Take(size)
                .ToList();
            List<ProjectItemViewModel> model = projects.Select(p => ProjectItemViewModel.FromEntity(p)).ToList();
            return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
        }

        public ResultViewModel<ProjectViewModel> GetById(int id)
        {
            Project? project = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .FirstOrDefault(x => x.Id == id);

            if (project == null) return ResultViewModel<ProjectViewModel>.Error("Projeto não existe") ;
            
            ProjectViewModel model = ProjectViewModel.fromEntity(project);
            return ResultViewModel<ProjectViewModel>.Success(model);
        }

        public ResultViewModel<int> Insert(CreateProjectInputModel model)
        {
            var project = model.ToEntity();
            _context.Projects.Add(project);
            _context.SaveChanges();
            return ResultViewModel<int>.Success(project.Id);
        }

        public ResultViewModel InsertComment(int id, CreateProjectCommentInputModel model)
        {
            Project project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null) return ResultViewModel.Error("Projeto não existe!");
            
            var comment = new ProjectComment(model.Content, model.IdProject, model.IdUser);
            _context.ProjectComments.Add(comment);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel Start(int id)
        {
            Project project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null) return ResultViewModel.Error("Projeto não existe");

            project.Start();
            _context.Projects.Update(project);
            _context.SaveChanges();
            return ResultViewModel.Success();
        }

        public ResultViewModel Update(UpdateProjectInputModel model)
        {
            Project project = _context.Projects.SingleOrDefault(p => p.Id == model.IdProject);
            if (project is null) return ResultViewModel.Error("Projeto não existe");
            project.Update(model.Title, model.Description, model.TotalCost);
            _context.SaveChanges();
            return ResultViewModel.Success();
        }
    }
}
