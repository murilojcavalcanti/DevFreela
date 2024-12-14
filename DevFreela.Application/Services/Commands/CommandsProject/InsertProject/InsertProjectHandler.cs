using DevFreela.Application.Models;
using DevFreela.Application.Notification.ProjectNotification;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Commands.CommandsProject.InsertProject
{
    public class InsertProjectHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _context;
        private readonly IMediator _mediator;
        public InsertProjectHandler(DevFreelaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
        {
            var project = request.ToEntity();
            _context.Projects.AddAsync(project);
            _context.SaveChangesAsync();
            var projectCreated = new ProjectCreatedNotification(project.Id,project.Title,project.TotalCost);
            _mediator.Publish(projectCreated);
            return ResultViewModel<int>.Success(project.Id);
        }
    }
}
