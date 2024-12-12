using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Commands.CommandsProject.UpdateProject
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;

        public UpdateProjectHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.IdProject);
            if (project is null) return ResultViewModel.Error("Projeto não existe");
            project.Update(request.Title, request.Description, request.TotalCost);
            _context.SaveChangesAsync();
            return ResultViewModel.Success();
        }
    }
}
