using DevFreela.Application.Models;
using DevFreela.Application.Services.Commands.CommandsProject.InsertCommentProject;
using DevFreela.Application.Services.Commands.CommandsProject.InsertProject;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Commands.CommandsProject.ValidateCommandsProject
{
    public class ValidateInsertProjectBehavior : IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _context;

        public ValidateInsertProjectBehavior(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken)
        {
            var ClientExists = _context.Users.Any(u => u.Id == request.IdClient);
            var FreelamcerExists = _context.Users.Any(u => u.Id == request.IdFreelancer);
            
            if (!ClientExists || !FreelamcerExists) return ResultViewModel<int>.Error("Cliente ou Freelancer Inválidos");
            
            return await next();
        }
    }
}
