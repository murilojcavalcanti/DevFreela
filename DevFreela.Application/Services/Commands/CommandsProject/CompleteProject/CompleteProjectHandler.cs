using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services.Commands.CommandsProject.CompleteProject
{
    public class CompleteProjectHandler : IRequestHandler<CompleteProjectCommand, ResultViewModel>
    {

        private readonly DevFreelaDbContext _context;

        public CompleteProjectHandler(DevFreelaDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<ResultViewModel> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = await _context.Projects.SingleOrDefaultAsync(p => p.Id ==request.Id);
            
            if (project is null) return ResultViewModel.Error("Projeto não existe!");
            
            project.Complete();
            _context.Projects.Update(project);
            _context.SaveChangesAsync();
            return ResultViewModel.Success();
        }
    }
}
