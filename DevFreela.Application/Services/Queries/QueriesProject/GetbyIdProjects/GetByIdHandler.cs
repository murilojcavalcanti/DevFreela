using DevFreela.Application.Models;
using DevFreela.Application.Models.project;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Queries.QueriesProject.GetbyIdProjects
{
    public class GetByIdHandler : IRequestHandler<GetbyIdQuery, ResultViewModel<ProjectViewModel>>
    {
        private readonly DevFreelaDbContext _context;

        public GetByIdHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<ProjectViewModel>> Handle(GetbyIdQuery request, CancellationToken cancellationToken)
        {
            Project? project = await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .SingleOrDefaultAsync(x => x.Id == request.Id);

            if (project == null) return ResultViewModel<ProjectViewModel>.Error("projeto não existe!");

            ProjectViewModel model = ProjectViewModel.fromEntity(project);
            return ResultViewModel<ProjectViewModel>.Success(model);
        }

    }
}
