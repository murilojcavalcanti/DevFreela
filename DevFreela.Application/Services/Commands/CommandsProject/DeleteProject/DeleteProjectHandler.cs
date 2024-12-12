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

namespace DevFreela.Application.Services.Commands.CommandsProject.DeleteProject
{
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, ResultViewModel>
    {
        private DevFreelaDbContext _context;

        public DeleteProjectHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            Project project =await  _context.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);
            if (project is null) return ResultViewModel.Error("Projeto não existe!");
            project.SetAsDeleted();
            _context.Projects.Update(project);
            _context.SaveChangesAsync(true);
            return ResultViewModel.Success();
        }
    }
}
