using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Repositories.ProjectRepositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAll(Expression<Func<Project, bool>> predicate);
        Task<Project> GetDetailsById(int id,int userId);
        Task<Project> GetById(int id);
        Task<bool> Exists(int id);
        Task<int> Add(Project project);
        Task<int> AddComment(ProjectComment project);
        Task Update(Project project);
        Task Delete(int id);

    }
}
