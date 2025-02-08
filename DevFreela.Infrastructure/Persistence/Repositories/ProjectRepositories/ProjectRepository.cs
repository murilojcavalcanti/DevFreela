using Azure.Core;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories.ProjectRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories.ProjectRepositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _context;

        public ProjectRepository(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Project project)
        {
            await _context.Projects.AddAsync(project);
            var projectCreated = _context.SaveChangesAsync();
            return projectCreated.Id;
        }

        public async Task<int> AddComment(ProjectComment comment)
        {
            
            _context.ProjectComments.AddAsync(comment);
            _context.SaveChangesAsync();
            return comment.Id;
        }

        public async Task Delete(int id)
        {
            Project project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == id);
            project.SetAsDeleted();
            _context.Projects.Update(project);
            _context.SaveChangesAsync(true);
        }
        public async Task<bool> Exists(int id)
        {
            return await _context.Projects.AnyAsync(p=>p.Id==id);
        }

        public async Task<List<Project>> GetAll()
        {
            var projects = await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .ToListAsync();
            return projects;
        }

        public async Task<Project> GetById(int id)
        {
            Project? project = await _context.Projects
                .SingleOrDefaultAsync(x => x.Id == id);
            return project;
        }

        public async Task<Project> GetDetailsById(int id)
        {
            Project? project = await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .SingleOrDefaultAsync(x => x.Id == id);
            return project;
        }

        public async Task Update(Project project)
        {
            _context.Projects.Update(project);
            _context.SaveChangesAsync();
        }

    }
}
