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

namespace DevFreela.Application.Services.Commands.CommandsProject.InsertCommentProject
{
    public class InsertCommentProjectHandler : IRequestHandler<InsertCommentProjectCommand, ResultViewModel>
    {

        private readonly DevFreelaDbContext _context;

        public InsertCommentProjectHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(InsertCommentProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.IdProject);
            if (project is null) return ResultViewModel.Error("Projeto não existe!");

            var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);
            _context.ProjectComments.AddAsync(comment);
            _context.SaveChangesAsync();

            return ResultViewModel.Success();

        }
    }
}
